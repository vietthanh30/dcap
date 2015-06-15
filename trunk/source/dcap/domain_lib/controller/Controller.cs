using System;
using System.Collections.Generic;
using core_lib.common;
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

        public IList<T> RetrieveAll<T>()
        {
            return m_PersistenceManager.RetrieveAll<T>(SessionAction.BeginAndEnd);
        }

        public void Save<T>(T item)
        {
            m_PersistenceManager.Save(item);
        }

        public UserDto login(string userName, string password)
        {
            return m_PersistenceManager.checkUser(userName, password);
        }

        public string changePassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            return m_PersistenceManager.changePassword(userName, oldPassword, newPassword, confirmPassword);
        }

        public string CreateUser(String parentId, String directParentId, String userName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl, string createdBy)
        {
            return m_PersistenceManager.CreateUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl, createdBy);
        }

        public string UpdateUser(String userName, String fullName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH, String photoUrl)
        {
            return m_PersistenceManager.UpdateUser(userName, fullName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH, photoUrl);
        }

        public string CreateUserAdmin(String userName, String fullName, String roleCode, string createdBy)
        {
            return m_PersistenceManager.CreateUserAdmin(userName, fullName, roleCode, createdBy);
        }

        public string SearchUser(String parentId, String directParentId, String userName, string ngaySinh, String soCmnd, string ngayCap, String soDienThoai, String diaChi, String gioiTinh, String soTaiKhoan,
            String chiNhanhNH)
        {
            return m_PersistenceManager.SearchUser(parentId, directParentId, userName, ngaySinh, soCmnd, ngayCap, soDienThoai, diaChi, gioiTinh, soTaiKhoan, chiNhanhNH);
        }

        public BangKeDto[] SearchBangKe(DateTime? thangKeKhai)
        {
            return m_PersistenceManager.SearchBangKe(thangKeKhai);
        }

        public BangKeDto[] SearchBangKeExt(string accountNumber, string userName, DateTime? beginDate, DateTime? endDate)
        {
            return m_PersistenceManager.SearchBangKeExt(accountNumber, userName, beginDate, endDate);
        }

        public HoaHongMemberDto[] SearchBangKeHoaHong(long accountNumber, DateTime? thangKeKhai)
        {
            return m_PersistenceManager.SearchBangKeHoaHong(accountNumber, thangKeKhai);
        }

        public UserDto[] SearchUserInfo(string soCmnd, string idThanhVien, string hoTen)
        {
            return m_PersistenceManager.SearchUserInfo(soCmnd, idThanhVien, hoTen);
        }

        public MemberNodeDto[] SearchMemberNodeDto(string accountNumber)
        {
            return m_PersistenceManager.SearchMemberNodeDto(accountNumber);
        }

        public MemberNodeDto[] SearchManagerNodeDto(string capQuanLy, string accountNumber)
        {
            return m_PersistenceManager.SearchManagerNodeDto(capQuanLy, accountNumber);
        }

        public bool IsContainMemberNode(long rootNumber, string accountNumber)
        {
            return m_PersistenceManager.IsContainMemberNode(rootNumber, accountNumber);
        }

        public MemberNodeDto GetNodeDto(string accountNumber)
        {
            return m_PersistenceManager.GetNodeDto(accountNumber);
        }

        public MemberNodeDto GetParentNodeByChildNo(string accountNumber, string parentField)
        {
            return m_PersistenceManager.GetParentNodeByChildNo(accountNumber, parentField);
        }

        public MemberNodeDto GetParentManagerNodeByChildNo(string capQuanLy, string accountNumber, string parentField)
        {
            var modelName = m_PersistenceManager.GetManagerModelName(capQuanLy);
            if (string.IsNullOrEmpty(modelName))
            {
                return null;
            }
            return m_PersistenceManager.GetParentNodeByChildNo(modelName, accountNumber, parentField);
        }

        public string UpdatePaid(BangKeDto[] bangKeDtos)
        {
            return m_PersistenceManager.UpdatePaid(bangKeDtos);
        }

		public ManagerApprovalDto[] SearchManagerApproval(string capQuanLy, string accountNumber)
		{
            return m_PersistenceManager.SearchManagerApproval(capQuanLy, accountNumber);
		}
		
		public string UpdateManagerApproval(ManagerApprovalDto dto)
		{
            return m_PersistenceManager.UpdateManagerApproval(dto);
		}

		public BonusApprovalDto[] SearchBonusApproval(string accountNumber, string userName, string isApproved)
		{
            return m_PersistenceManager.SearchBonusApproval(accountNumber, userName, isApproved);
		}
		
		public string CreateBonusApproval(BonusApprovalDto dto)
		{
            return m_PersistenceManager.CreateBonusApproval(dto);
		}

		public string UpdateBonusApproval(BonusApprovalDto dto)
		{
            return m_PersistenceManager.UpdateBonusApproval(dto);
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
                        if (parentAccount != null && account.AccountId != parentAccount.AccountId)
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
            catch (Exception ex)
            {
                throw new Exception("CalculateAccountLog Error: " + ex.Message);
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
                                  BonusType = (account.ParentId == account.ParentDirectId && account.ParentDirectId == calculatedAccount.AccountId) ? "TT" : "LK"
                              };

            m_PersistenceManager.Save(preCalc);

            if (account.ParentDirectId == calculatedAccount.AccountId && account.ParentId != account.ParentDirectId)
            {
                preCalc = new AccountPreCalc
                {
                    AccountId = account.AccountId,
                    CalcAccountId = calculatedAccount.AccountId,
                    AccountLevel = -1,
                    IsCalculated = "N",
                    LevelIndex = -1,
                    CreatedDate = DateTime.Now,
                    BonusType = "TT"
                };

                m_PersistenceManager.Save(preCalc);
            }

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
                    // truc tiep);
                    if (accountPreCalc.BonusType == "TT")
                    {
                        CalculateTtBonus(accountPreCalc);
                    }

                    if (IsCalculated(accountPreCalc))
                    {
                        // can cap
                        if (((accountPreCalc.LevelIndex % 3) != 1) && (accountPreCalc.AccountLevel < 4))
                        {
                            CalculateCcBonus(accountPreCalc);
                        }

                        // ma roi
                        if (accountPreCalc.AccountLevel>1 )
                        {
                            CalculateHtBonus(accountPreCalc);
                        }

                        // insert into QL1 tree
                        if (accountPreCalc.AccountLevel == 3 
                            && m_PersistenceManager.CountCalculatedByLevel(accountPreCalc.CalcAccountId, 3) == 26)
                        {
                            AddToQl1Tree(accountPreCalc.CalcAccountId);
                        }
                    }

                    //mark calculated
                    accountPreCalc.IsCalculated = "Y";
                    accountPreCalc.CalculatedDate = DateTime.Now;
                    m_PersistenceManager.Save(accountPreCalc);
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw new Exception("CalculateBonusOfAccountTree Error: " + ex.Message);
            }
        }

        public int ExecuteApprovedManager()
        {
            //read approved manager
            IList<ManagerApproval> approvedList = m_PersistenceManager.GetApprovedManager();
            foreach (var approvedManager in approvedList)
            {
                
                
                m_PersistenceManager.Delete(approvedManager);
            }
            return 1;
        }

        public int CalculateBonusOfManagerTree()
        {
            try
            {
                //read manager level tree log
                IList<ManagerLevelLog> managerLog = m_PersistenceManager.GetManagerLog();

                if (managerLog == null || managerLog.Count == 0)
                    return 0;

                foreach (var log in managerLog)
                {
                    //calculate QL1
                    if (log.Level==1)
                    {
                        var newManager = (ManagerL1) m_PersistenceManager.GetManagerLevel<ManagerL1>(log.AccountId);
                        if (newManager.Level != 0)
                            CalculateBonusOfQl1(newManager);
                    }

                    //calculate QL2
                    if (log.Level == 2)
                    {
                        var newManager = (ManagerL2)m_PersistenceManager.GetManagerLevel<ManagerL2>(log.AccountId);
                        if (newManager.Level != 0)
                            CalculateBonusOfQl2(newManager);
                    }

                    //calculate QL3
                    if (log.Level == 3)
                    {
                        var newManager = (ManagerL3)m_PersistenceManager.GetManagerLevel<ManagerL3>(log.AccountId);
                        if (newManager.Level != 0)
                            CalculateBonusOfQl3(newManager);
                    }

                    //calculate QL4
                    if (log.Level == 4)
                    {
                        var newManager = (ManagerL4)m_PersistenceManager.GetManagerLevel<ManagerL4>(log.AccountId);
                        if (newManager.Level != 0)
                            CalculateBonusOfQl4(newManager);
                    }

                    //calculate QL5
                    if (log.Level == 5)
                    {
                        var newManager = (ManagerL5)m_PersistenceManager.GetManagerLevel<ManagerL5>(log.AccountId);
                        if (newManager.Level != 0)
                            CalculateBonusOfQl5(newManager);
                    }

                    //calculate QL6
                    if (log.Level == 6)
                    {
                        var newManager = (ManagerL6)m_PersistenceManager.GetManagerLevel<ManagerL6>(log.AccountId);
                        if (newManager.Level != 0)
                            CalculateBonusOfQl6(newManager);
                    }

                    // delete log
                    m_PersistenceManager.Delete(log);
                }
                
                return 1;
            }catch(Exception ex)
            {
                throw new Exception("CalculateBonusOfManagerTree Error: " + ex.Message);
            }
        }

        private void CalculateBonusOfQl1(ManagerL1 newManager)
        {
            // Hoa hong quan ly
            m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_QL1.Amount,
                                                  ConstUtil.BONUS_TYPE_QL1.Type);
                        
            var managerLevel = (ManagerL1) m_PersistenceManager.GetManagerLevel<ManagerL1>(newManager.ParentId);
            if (managerLevel != null && managerLevel.ParentId !=-1)
                m_PersistenceManager.SaveAccountBonus(managerLevel.ParentId, ConstUtil.BONUS_TYPE_QL1.Amount,
                                                      ConstUtil.BONUS_TYPE_QL1.Type);
            // Can cap
            if ((newManager.LevelIndex%3)!=1)
            {
                m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_CCL1.Amount,
                                                      ConstUtil.BONUS_TYPE_CCL1.Type);
                if (managerLevel != null && managerLevel.ParentId != -1)
                    m_PersistenceManager.SaveAccountBonus(managerLevel.ParentId, ConstUtil.BONUS_TYPE_CCL1.Amount,
                                                          ConstUtil.BONUS_TYPE_CCL1.Type);
            }

            // move to QL2 approved queue
            if (newManager.LevelIndex%9==0)
            {
                if (managerLevel != null && managerLevel.ParentId != -1)
                {
                    InsertToQlApproveQueue(managerLevel.ParentId, 2);
                }
            }
        }

        private void CalculateBonusOfQl3(ManagerL3 newManager)
        {
            // Hoa hong quan ly
            m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_QL3.Amount,
                                                  ConstUtil.BONUS_TYPE_QL3.Type);

            var managerLevel = (ManagerL3)m_PersistenceManager.GetManagerLevel<ManagerL3>(newManager.ParentId);
            if (managerLevel != null && managerLevel.ParentId != -1)
                m_PersistenceManager.SaveAccountBonus(managerLevel.ParentId, ConstUtil.BONUS_TYPE_QL3.Amount,
                                                      ConstUtil.BONUS_TYPE_QL3.Type);
            // Can cap
            if ((newManager.LevelIndex % 3) != 1)
            {
                m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_CCL3.Amount,
                                                      ConstUtil.BONUS_TYPE_CCL3.Type);
                if (managerLevel != null && managerLevel.ParentId != -1)
                    m_PersistenceManager.SaveAccountBonus(managerLevel.ParentId, ConstUtil.BONUS_TYPE_CCL3.Amount,
                                                          ConstUtil.BONUS_TYPE_CCL3.Type);
            }

            // move to QL4 approved queue
            if (newManager.LevelIndex % 9 == 0)
            {
                if (managerLevel != null && managerLevel.ParentId != -1)
                {
                    InsertToQlApproveQueue(managerLevel.ParentId, 4);
                }
            }
        }

        private void CalculateBonusOfQl5(ManagerL5 newManager)
        {
            // Hoa hong quan ly
            m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_QL5.Amount,
                                                  ConstUtil.BONUS_TYPE_QL5.Type);

            var managerLevel = (ManagerL5)m_PersistenceManager.GetManagerLevel<ManagerL5>(newManager.ParentId);
            if (managerLevel != null && managerLevel.ParentId != -1)
                m_PersistenceManager.SaveAccountBonus(managerLevel.ParentId, ConstUtil.BONUS_TYPE_QL5.Amount,
                                                      ConstUtil.BONUS_TYPE_QL5.Type);
            // Can cap
            if ((newManager.LevelIndex % 3) != 1)
            {
                m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_CCL5.Amount,
                                                      ConstUtil.BONUS_TYPE_CCL5.Type);
                if (managerLevel != null && managerLevel.ParentId != -1)
                    m_PersistenceManager.SaveAccountBonus(managerLevel.ParentId, ConstUtil.BONUS_TYPE_CCL5.Amount,
                                                          ConstUtil.BONUS_TYPE_CCL5.Type);
            }

            // move to QL6 approved queue
            if (newManager.LevelIndex % 9 == 0)
            {
                if (managerLevel != null && managerLevel.ParentId != -1)
                {
                    InsertToQlApproveQueue(managerLevel.ParentId, 6);
                }
            }
        }

        private void CalculateBonusOfQl2(ManagerL2 newManager)
        {
            // Hoa hong quan ly
            m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_QL2.Amount,
                                                  ConstUtil.BONUS_TYPE_QL2.Type);

            var firstUpLevel = (ManagerL2)m_PersistenceManager.GetManagerLevel<ManagerL2>(newManager.ParentId);
            ManagerL2 secondUpLevel = null;
            if (firstUpLevel != null && firstUpLevel.Level != 0)
            {
                m_PersistenceManager.SaveAccountBonus(firstUpLevel.ParentId, ConstUtil.BONUS_TYPE_QL2.Amount,
                                                      ConstUtil.BONUS_TYPE_QL2.Type);
                secondUpLevel = (ManagerL2) m_PersistenceManager.GetManagerLevel<ManagerL2>(firstUpLevel.ParentId);
                if (secondUpLevel != null && secondUpLevel.Level != 0)
                {
                    m_PersistenceManager.SaveAccountBonus(secondUpLevel.ParentId, ConstUtil.BONUS_TYPE_QL2.Amount,
                                                      ConstUtil.BONUS_TYPE_QL2.Type);
                }
            }
                
            // Can cap
            if ((newManager.LevelIndex % 3) != 1)
            {
                m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_CCL2.Amount,
                                                      ConstUtil.BONUS_TYPE_CCL2.Type);
                if (firstUpLevel != null && firstUpLevel.ParentId != -1)
                {
                    m_PersistenceManager.SaveAccountBonus(firstUpLevel.ParentId, ConstUtil.BONUS_TYPE_CCL2.Amount,
                                                          ConstUtil.BONUS_TYPE_CCL2.Type);
                    if (secondUpLevel != null && secondUpLevel.Level != 0)
                    {
                        m_PersistenceManager.SaveAccountBonus(secondUpLevel.ParentId, ConstUtil.BONUS_TYPE_CCL2.Amount,
                                                          ConstUtil.BONUS_TYPE_CCL2.Type);
                    }
                }
                    
            }

            // move to QL3 approved queue
            if (newManager.LevelIndex % 27 == 0)
            {
                if (secondUpLevel != null && secondUpLevel.ParentId != -1)
                {
                    InsertToQlApproveQueue(secondUpLevel.ParentId, 3);
                }
            }
        }

        private void CalculateBonusOfQl4(ManagerL4 newManager)
        {
            // Hoa hong quan ly
            m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_QL4.Amount,
                                                  ConstUtil.BONUS_TYPE_QL4.Type);

            var firstUpLevel = (ManagerL4)m_PersistenceManager.GetManagerLevel<ManagerL4>(newManager.ParentId);
            ManagerL4 secondUpLevel = null;
            if (firstUpLevel != null && firstUpLevel.Level != 0)
            {
                m_PersistenceManager.SaveAccountBonus(firstUpLevel.ParentId, ConstUtil.BONUS_TYPE_QL4.Amount,
                                                      ConstUtil.BONUS_TYPE_QL4.Type);
                secondUpLevel = (ManagerL4)m_PersistenceManager.GetManagerLevel<ManagerL4>(firstUpLevel.ParentId);
                if (secondUpLevel != null && secondUpLevel.Level != 0)
                {
                    m_PersistenceManager.SaveAccountBonus(secondUpLevel.ParentId, ConstUtil.BONUS_TYPE_QL4.Amount,
                                                      ConstUtil.BONUS_TYPE_QL4.Type);
                }
            }

            // Can cap
            if ((newManager.LevelIndex % 3) != 1)
            {
                m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_CCL4.Amount,
                                                      ConstUtil.BONUS_TYPE_CCL4.Type);
                if (firstUpLevel != null && firstUpLevel.ParentId != -1)
                {
                    m_PersistenceManager.SaveAccountBonus(firstUpLevel.ParentId, ConstUtil.BONUS_TYPE_CCL4.Amount,
                                                          ConstUtil.BONUS_TYPE_CCL4.Type);
                    if (secondUpLevel != null && secondUpLevel.Level != 0)
                    {
                        m_PersistenceManager.SaveAccountBonus(secondUpLevel.ParentId, ConstUtil.BONUS_TYPE_CCL4.Amount,
                                                          ConstUtil.BONUS_TYPE_CCL4.Type);
                    }
                }
            }

            // move to QL5 approved queue
            if (newManager.LevelIndex % 27 == 0)
            {
                if (secondUpLevel != null && secondUpLevel.ParentId != -1)
                {
                    InsertToQlApproveQueue(secondUpLevel.ParentId, 5);
                }
            }
        }

        private void CalculateBonusOfQl6(ManagerL6 newManager)
        {
            // Hoa hong quan ly
            m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_QL6.Amount,
                                                  ConstUtil.BONUS_TYPE_QL6.Type);

            var firstUpLevel = (ManagerL6)m_PersistenceManager.GetManagerLevel<ManagerL6>(newManager.ParentId);
            ManagerL6 secondUpLevel = null;
            if (firstUpLevel != null && firstUpLevel.Level != 0)
            {
                m_PersistenceManager.SaveAccountBonus(firstUpLevel.ParentId, ConstUtil.BONUS_TYPE_QL6.Amount,
                                                      ConstUtil.BONUS_TYPE_QL6.Type);
                secondUpLevel = (ManagerL6)m_PersistenceManager.GetManagerLevel<ManagerL4>(firstUpLevel.ParentId);
                if (secondUpLevel != null && secondUpLevel.Level != 0)
                {
                    m_PersistenceManager.SaveAccountBonus(secondUpLevel.ParentId, ConstUtil.BONUS_TYPE_QL6.Amount,
                                                      ConstUtil.BONUS_TYPE_QL6.Type);
                }
            }

            // Can cap
            if ((newManager.LevelIndex % 3) != 1)
            {
                m_PersistenceManager.SaveAccountBonus(newManager.ParentId, ConstUtil.BONUS_TYPE_CCL6.Amount,
                                                      ConstUtil.BONUS_TYPE_CCL6.Type);
                if (firstUpLevel != null && firstUpLevel.ParentId != -1)
                {
                    m_PersistenceManager.SaveAccountBonus(firstUpLevel.ParentId, ConstUtil.BONUS_TYPE_CCL6.Amount,
                                                          ConstUtil.BONUS_TYPE_CCL6.Type);
                    if (secondUpLevel != null && secondUpLevel.Level != 0)
                    {
                        m_PersistenceManager.SaveAccountBonus(secondUpLevel.ParentId, ConstUtil.BONUS_TYPE_CCL6.Amount,
                                                          ConstUtil.BONUS_TYPE_CCL6.Type);
                    }
                }
            }
        }

        private void InsertToQlApproveQueue(long accountId, int managerLevel)
        {
            var approvedQueue = new ManagerApproval()
                                    {
                                        AccountId = accountId,
                                        ManagerLevel = managerLevel,
                                        IsApproved = "I",
                                        CreatedDate = DateTime.Now
                                    };
            m_PersistenceManager.Save(approvedQueue);
        }

        private ManagerL1 AddToQl1Tree(long calcAccountId)
        {
            // Get level and level_index
            var lastestChild = (ManagerL1) m_PersistenceManager.GetQlLatestChild<ManagerL1>();
            
            // Find Parent account
            if (lastestChild == null)
            {
                var root = new ManagerL1
                               {
                                   AccountId = calcAccountId,
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

                if ((lastestChild.Level==0) || (Math.Pow(3, lastestChild.Level) == lastestChild.LevelIndex))
                {
                    newLevel = lastestChild.Level + 1;
                    newLevelIndex = 1;
                }

                var l1ParentLevel = newLevel - 1;
                var l1ParentLevelIndex = (newLevelIndex%3 > 0) ? ((newLevelIndex/3)+1) : (newLevelIndex/3);
                if (newLevel == 1)
                {
                    l1ParentLevel = 0;
                    l1ParentLevelIndex = 0;
                }
                

                var managerParent = (ManagerL1) m_PersistenceManager.FindQlByLocation<ManagerL1>(l1ParentLevel, l1ParentLevelIndex);
                
                // Insert into QL1 tree
                var newNode = new ManagerL1
                {
                    AccountId = calcAccountId,
                    ChildIndex = (newLevelIndex % 3) == 0 ? 3 : newLevelIndex % 3,
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

        private ManagerL2 AddToQl2Tree(long calcAccountId)
        {

            // Get level and level_index
            var lastestChild = (ManagerL2) m_PersistenceManager.GetQlLatestChild<ManagerL2>();
            
            // Find Parent account
            if (lastestChild == null)
            {
                var root = new ManagerL2
                               {
                                   AccountId = calcAccountId,
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

                if ((lastestChild.Level == 0) || (Math.Pow(3, lastestChild.Level) == lastestChild.LevelIndex))
                {
                    newLevel = lastestChild.Level + 1;
                    newLevelIndex = 1;
                }

                var l1ParentLevel = newLevel - 1;
                var l1ParentLevelIndex = (newLevelIndex%3 > 0) ? ((newLevelIndex/3) + 1) : (newLevelIndex/3);
                if (newLevel == 1)
                {
                    l1ParentLevel = 0;
                    l1ParentLevelIndex = 0;
                }

                var managerParent = (ManagerL2) m_PersistenceManager.FindQlByLocation<ManagerL2>(l1ParentLevel, l1ParentLevelIndex);

                // Insert into QL2 tree
                var newNode = new ManagerL2
                                  {
                                      AccountId = calcAccountId,
                                      ChildIndex = (newLevelIndex % 3) == 0 ? 3 : newLevelIndex % 3,
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

        private ManagerL3 AddToQl3Tree(long calcAccountId)
        {

            // Get level and level_index
            var lastestChild = (ManagerL3)m_PersistenceManager.GetQlLatestChild<ManagerL3>();

            // Find Parent account
            if (lastestChild == null)
            {
                var root = new ManagerL3
                {
                    AccountId = calcAccountId,
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

                var parentLevel = newLevel - 1;
                var parentLevelIndex = (newLevelIndex % 3 > 0) ? ((newLevelIndex / 3) + 1) : (newLevelIndex / 3);

                var managerParent = (ManagerL3)m_PersistenceManager.FindQlByLocation<ManagerL3>(parentLevel, parentLevelIndex);

                // Insert into QL3 tree
                var newNode = new ManagerL3
                {
                    AccountId = calcAccountId,
                    ChildIndex = (newLevelIndex % 3) == 0 ? 3 : newLevelIndex % 3,
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

        private ManagerL4 AddToQl4Tree(long calcAccountId)
        {

            // Get level and level_index
            var lastestChild = (ManagerL4)m_PersistenceManager.GetQlLatestChild<ManagerL4>();

            // Find Parent account
            if (lastestChild == null)
            {
                var root = new ManagerL4
                {
                    AccountId = calcAccountId,
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

                var parentLevel = newLevel - 1;
                var parentLevelIndex = (newLevelIndex % 3 > 0) ? ((newLevelIndex / 3) + 1) : (newLevelIndex / 3);

                var managerParent = (ManagerL4)m_PersistenceManager.FindQlByLocation<ManagerL4>(parentLevel, parentLevelIndex);

                // Insert into QL4 tree
                var newNode = new ManagerL4
                {
                    AccountId = calcAccountId,
                    ChildIndex = (newLevelIndex % 3) == 0 ? 3 : newLevelIndex % 3,
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


        private ManagerL5 AddToQl5Tree(long calcAccountId)
        {

            // Get level and level_index
            var lastestChild = (ManagerL5)m_PersistenceManager.GetQlLatestChild<ManagerL5>();

            // Find Parent account
            if (lastestChild == null)
            {
                var root = new ManagerL5
                {
                    AccountId = calcAccountId,
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

                var parentLevel = newLevel - 1;
                var parentLevelIndex = (newLevelIndex % 3 > 0) ? ((newLevelIndex / 3) + 1) : (newLevelIndex / 3);

                var managerParent = (ManagerL5)m_PersistenceManager.FindQlByLocation<ManagerL5>(parentLevel, parentLevelIndex);

                // Insert into QL5 tree
                var newNode = new ManagerL5
                {
                    AccountId = calcAccountId,
                    ChildIndex = (newLevelIndex % 3) == 0 ? 3 : newLevelIndex % 3,
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

        private ManagerL6 AddToQl6Tree(long calcAccountId)
        {

            // Get level and level_index
            var lastestChild = (ManagerL6)m_PersistenceManager.GetQlLatestChild<ManagerL6>();

            // Find Parent account
            if (lastestChild == null)
            {
                var root = new ManagerL6
                {
                    AccountId = calcAccountId,
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

                var parentLevel = newLevel - 1;
                var parentLevelIndex = (newLevelIndex % 3 > 0) ? ((newLevelIndex / 3) + 1) : (newLevelIndex / 3);

                var managerParent = (ManagerL6)m_PersistenceManager.FindQlByLocation<ManagerL6>(parentLevel, parentLevelIndex);

                // Insert into QL6 tree
                var newNode = new ManagerL6
                {
                    AccountId = calcAccountId,
                    ChildIndex = (newLevelIndex % 3) == 0 ? 3 : newLevelIndex % 3,
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
            m_PersistenceManager.SaveAccountBonus(accountPreCalc.CalcAccountId, 0.8, "TT");
        }

        private bool IsCalculated(AccountPreCalc accountPreCalc)
        {
            if (accountPreCalc.AccountLevel == -1)
                return false;
            if (accountPreCalc.AccountLevel == 1 && accountPreCalc.BonusType == "TT")
                return true;
            if (accountPreCalc.AccountLevel == 1 && accountPreCalc.BonusType == "LK" && accountPreCalc.LevelIndex > 1)
                return true;
            return m_PersistenceManager.CountUpLevel(accountPreCalc.CalcAccountId, accountPreCalc.AccountLevel - 1) == Math.Pow(3, accountPreCalc.AccountLevel - 1) 
                /*&& m_PersistenceManager.CountLeft(accountPreCalc.CalcAccountId, accountPreCalc.AccountLevel, accountPreCalc.LevelIndex) == (accountPreCalc.LevelIndex - 1)s*/;
        }

        public long GetMemberAmount()
        {
            return m_PersistenceManager.GetRowCount("MemberInfo");
        }

        public long GetAccountAmount()
        {
            return m_PersistenceManager.GetRowCount("Account");
        }

        public string GetManagerAmount()
        {
            var managerL1 = m_PersistenceManager.GetRowCount("ManagerL1");
            var managerL2 = m_PersistenceManager.GetRowCount("ManagerL2");
            var managerL3 = m_PersistenceManager.GetRowCount("ManagerL3");
            var managerL4 = m_PersistenceManager.GetRowCount("ManagerL4");
            var managerL5 = m_PersistenceManager.GetRowCount("ManagerL5");
            var managerL6 = m_PersistenceManager.GetRowCount("ManagerL6");
            return "Cấp 1: " + managerL1 + "|Cấp 2: " + managerL2 + "|Cấp 3: " + managerL3 + "<br/>Cấp 4: " + managerL4 + "|Cấp 5: " + managerL5 + "|Cấp 6: " + managerL6;
        }

        public long GetManagerL6Amount()
        {
            return m_PersistenceManager.GetRowCount("ManagerL6");
        }

        public UserDto[] GetNewMemberList()
        {
            return m_PersistenceManager.GetNewMemberList();
        }

        public UserDto[] GetNewManagerList()
        {
            return m_PersistenceManager.GetNewManagerList();
        }

        public int GetReportYear()
        {
            return m_PersistenceManager.GetReportYear();
        }

        public AccountBonusDto[] GetAcountBonusList()
        {
            return m_PersistenceManager.GetAcountBonusList();
        }

        #endregion
    }
}