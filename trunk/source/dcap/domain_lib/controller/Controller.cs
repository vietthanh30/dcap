using System;
using System.Collections.Generic;
using domain_lib.dto;
using domain_lib.model;
using domain_lib.persistence;

namespace domain_lib.controller
{
    public class Controller
    {
        #region Declarations

        // Member variables
        private PersistenceManager m_PersistenceManager = new PersistenceManager();
        
        // Property variables

        #endregion

        #region Constructor

        public Controller()
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public string login(string userName, string password)
        {
            return m_PersistenceManager.checkUser(userName, password);
        }

        public string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            return m_PersistenceManager.changePassword(userName, oldPassword, newPassword, confirmPassword);
        }

        public string CreateUser(String parentId, String directParentId, String userName, DateTime ngaySinh, String soCmnd, DateTime ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            return m_PersistenceManager.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
        }

        public string SearchUser(String parentId, String directParentId, String userName, DateTime ngaySinh, String soCmnd, DateTime ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH)
        {
            return m_PersistenceManager.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH);
        }


        public int CalculateAccountLog()
        {
            try
            {
                IList<AccountLog> accountLog = m_PersistenceManager.GetAccountLog();
                if (accountLog == null || accountLog.Count == 0)
                    return 0;

                foreach (var log in accountLog)
                {
                    try
                    {
                        Account account = m_PersistenceManager.GetAccount(log.AccountId);
                        Account parentAccount = m_PersistenceManager.GetAccount(account.ParentId);
                        if (parentAccount == null || account.AccountId == parentAccount.AccountId)
                            continue;
                        PutToPreCalcQueue(account, parentAccount, 1, account.ChildIndex);
                        var hist = new AccountLogHist
                        {
                            AccountId = log.AccountId,
                            Dml = log.Dml,
                            CreatedDate = DateTime.Now
                        };
                        m_PersistenceManager.Save(hist);
                        m_PersistenceManager.Delete(log);
                    }
                    catch (Exception ex)
                    {
                        var error = new AccountLogHist
                                        {
                                            AccountId = log.AccountId,
                                            Dml = log.Dml,
                                            Error = ex.Message,
                                            CreatedDate = DateTime.Now
                                        };
                        m_PersistenceManager.Save(error);
                        m_PersistenceManager.Delete(log);
                    }
                }

                return 1;

            }
            catch (Exception)
            {
                return -1;
            }
        }

        private void PutToPreCalcQueue(Account account, Account calculatedAccount, int level, long levelIndex)
        {
            // insert preparation calculated queue
            var preCalc = new AccountPreCalc
                              {
                                  AccountId = account.AccountId,
                                  CalcAccountId = calculatedAccount.AccountId,
                                  AccountLevel = level,
                                  IsCalculated = "N",
                                  LevelIndex = levelIndex,
                                  CreatedDate = DateTime.Now,
                                  BonusType = account.ParentDirectId == calculatedAccount.AccountId ? "TT" : "LK"
                              };

            m_PersistenceManager.Save(preCalc);

            // calculate next level
            Account nextLevelAccount = m_PersistenceManager.GetAccount(calculatedAccount.ParentId);
            if (nextLevelAccount == null || nextLevelAccount.AccountId == calculatedAccount.AccountId)
                return;
            int nextLevel = level + 1;
            var nextLevelIndex = (long)((calculatedAccount.ChildIndex - 1) * Math.Pow(3, nextLevel - 1) + levelIndex);
            PutToPreCalcQueue(account, nextLevelAccount, nextLevel, nextLevelIndex);
        }

        public int CalculateBonusOfAccountTree()
        {
            try
            {
                IList<AccountPreCalc> preCalcQueue = m_PersistenceManager.GetPreCalcQueue();
                if (preCalcQueue == null || preCalcQueue.Count == 0)
                    return 0;

                foreach (var accountPreCalc in preCalcQueue)
                {
                    if (IsCalculated(accountPreCalc))
                    {
                        // truc tiep
                        if (accountPreCalc.BonusType == "TT")
                        {
                            CalculateTtBonus(accountPreCalc);
                        }

                        // can cap
                        if ((accountPreCalc.LevelIndex % 3)!=1)
                        {
                            CalculateCcBonus(accountPreCalc);
                        }

                        // ma roi
                        if (accountPreCalc.AccountLevel>1)
                        {
                            CalculateMrBonus(accountPreCalc);
                        }

                        // he thong
                        if (accountPreCalc.AccountLevel>1)
                        {
                            CalculateHtBonus(accountPreCalc);
                        }

                        // insert into QL1 tree
                        if (accountPreCalc.AccountLevel == 2 && accountPreCalc.LevelIndex == 9)
                        {
                            AddToQl1Tree(accountPreCalc);
                        }

                        // insert into QL2 tree
                        if (accountPreCalc.AccountLevel == 3 && accountPreCalc.LevelIndex == 27)
                        {
                            AddToQl2Tree(accountPreCalc);
                        }

                        //mark calculated
                        accountPreCalc.IsCalculated = "Y";
                        accountPreCalc.CalculatedDate = DateTime.Now;
                        m_PersistenceManager.Save(accountPreCalc);
                    }
                }
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private ManagerL1 AddToQl1Tree(AccountPreCalc accountPreCalc)
        {
            // Get level and level_index
            var lastestChild = (ManagerL1) m_PersistenceManager.GetQlLatestChild<ManagerL1>();
            
            // Find Parent account
            if (lastestChild == null)
            {
                var root = new ManagerL1
                               {
                                   AccountId = accountPreCalc.CalcAccountId,
                                   ChildIndex = 0,
                                   Level = 0,
                                   LevelIndex = 0,
                                   IsActive = "Y",
                                   ParentId = -1,
                                   CreatedBy = "JOB",
                                   CreatedDate = DateTime.Now
                               };
                m_PersistenceManager.Save(root);
                return root;
            }else
            {

                var newLevel = lastestChild.Level;
                var newLevelIndex = lastestChild.LevelIndex + 1;
               
                if (Math.Pow(3,lastestChild.Level)==lastestChild.LevelIndex)
                {
                    newLevel = lastestChild.Level + 1;
                    newLevelIndex = 1;
                }

                var l1ParentLevel = newLevel - 1;
                var l1ParentLevelIndex = (newLevelIndex%3 > 0) ? ((newLevelIndex/3)+1) : (newLevelIndex/3);

                var managerParent = (ManagerL1) m_PersistenceManager.FindQlByLocation<ManagerL1>(l1ParentLevel, l1ParentLevelIndex);
                
                // Insert into QL1 tree
                var newNode = new ManagerL1
                {
                    AccountId = accountPreCalc.CalcAccountId,
                    ChildIndex = (newLevelIndex/3)+1,
                    Level = newLevel,
                    LevelIndex = newLevelIndex,
                    IsActive = "Y",
                    ParentId = managerParent.AccountId,
                    CreatedBy = "JOB",
                    CreatedDate = DateTime.Now
                };
                m_PersistenceManager.Save(newNode);
                
                return newNode;
            }
        }

        private ManagerL2 AddToQl2Tree(AccountPreCalc accountPreCalc)
        {

            // Get level and level_index
            var lastestChild = (ManagerL2) m_PersistenceManager.GetQlLatestChild<ManagerL2>();
            
            // Find Parent account
            if (lastestChild == null)
            {
                var root = new ManagerL2
                               {
                                   AccountId = accountPreCalc.CalcAccountId,
                                   ChildIndex = 0,
                                   Level = 0,
                                   LevelIndex = 0,
                                   IsActive = "Y",
                                   ParentId = -1,
                                   CreatedBy = "JOB",
                                   CreatedDate = DateTime.Now
                               };
                m_PersistenceManager.Save(root);
                return root;
            }
            else
            {

                var newLevel = lastestChild.Level;
                var newLevelIndex = lastestChild.LevelIndex + 1;

                if (Math.Pow(3, lastestChild.Level) == lastestChild.LevelIndex)
                {
                    newLevel = lastestChild.Level + 1;
                    newLevelIndex = 1;
                }

                var l1ParentLevel = newLevel - 1;
                var l1ParentLevelIndex = (newLevelIndex%3 > 0) ? ((newLevelIndex/3) + 1) : (newLevelIndex/3);

                var managerParent = (ManagerL2) m_PersistenceManager.FindQlByLocation<ManagerL2>(l1ParentLevel, l1ParentLevelIndex);

                // Insert into QL2 tree
                var newNode = new ManagerL2
                                  {
                                      AccountId = accountPreCalc.CalcAccountId,
                                      ChildIndex = (newLevelIndex/3) + 1,
                                      Level = newLevel,
                                      LevelIndex = newLevelIndex,
                                      IsActive = "Y",
                                      ParentId = managerParent.AccountId,
                                      CreatedBy = "JOB",
                                      CreatedDate = DateTime.Now
                                  };
                m_PersistenceManager.Save(newNode);

                return newNode;
            }
        }

        private void CalculateHtBonus(AccountPreCalc accountPreCalc)
        {
            double bonusAmmount;
            switch(accountPreCalc.AccountLevel)
            {   
                case 2:
                    bonusAmmount = 0.2;
                    break;
                case 3:
                    bonusAmmount = 0.2;
                    break;
                case 4:
                    bonusAmmount = 0.03;
                    break;
                case 5:
                    bonusAmmount = 0.01;
                    break;
                case 6:
                    bonusAmmount = 0.005;
                    break;
                case 7:
                    bonusAmmount = 0.003;
                    break;
                default:
                    bonusAmmount = 0.001;
                    break;
            }

            m_PersistenceManager.SaveAccountBonus(accountPreCalc.CalcAccountId, bonusAmmount, "HT");
        }

        private void CalculateMrBonus(AccountPreCalc accountPreCalc)
        {
            m_PersistenceManager.SaveAccountBonus(accountPreCalc.CalcAccountId, 0.2, "MR");
        }

        private void CalculateCcBonus(AccountPreCalc accountPreCalc)
        {
            m_PersistenceManager.SaveAccountBonus(accountPreCalc.CalcAccountId, 0.2, "CC");
        }

        private void CalculateTtBonus(AccountPreCalc accountPreCalc)
        {
            m_PersistenceManager.SaveAccountBonus(accountPreCalc.CalcAccountId, 1.2, "TT");
        }

        private bool IsCalculated(AccountPreCalc accountPreCalc)
        {
            if (accountPreCalc.AccountLevel == 1 && accountPreCalc.BonusType == "TT")
                return true;
            if (accountPreCalc.AccountLevel == 1 && accountPreCalc.BonusType == "LK" && accountPreCalc.LevelIndex > 1)
                return true;
            return m_PersistenceManager.CountUpLevel(accountPreCalc.CalcAccountId, accountPreCalc.AccountLevel) == Math.Pow(3, accountPreCalc.AccountLevel - 1) && m_PersistenceManager.CountLeft(accountPreCalc.CalcAccountId, accountPreCalc.AccountLevel, accountPreCalc.LevelIndex) == (accountPreCalc.LevelIndex - 1);
        }
        
        #endregion
    }
}