using Aurae_Facebook_Care.Common;
using Aurae_Facebook_Care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurae_Facebook_Care.DAO
{
    public class AccountDAO : BaseDAO
    {
        public ECodeInfo Add(Account account)
        {
            var cAccount = Get(account.U);

            if (cAccount != null)
            {
                return ECode.ACCOUNT_EXIST;
            }

            _context.Add(account);

            var result = SaveChanges();

            return result;
        }

        public ECodeInfo Update(Account account)
        {
            var cAccount = Get(account.U);

            if (cAccount == null)
            {
                return ECode.ACCOUNT_NOT_FOUND;
            }

            cAccount.P = account.P;
            cAccount.C = account.C;
            cAccount.F = account.F;
            cAccount.Email = account.Email;
            cAccount.EmailPassword = account.EmailPassword;
            cAccount.RecoveryEmail = account.RecoveryEmail;
            cAccount.RecoveryEmailPassword = account.RecoveryEmailPassword;
            cAccount.Phone = account.Phone;
            cAccount.Name = account.Name;
            cAccount.Birthday = account.Birthday;
            cAccount.Friends = account.Friends;
            cAccount.RequestSent = account.RequestSent;
            cAccount.RequestReceive = account.RequestReceive;
            cAccount.Suggestion = account.Suggestion;
            cAccount.Groups = account.Groups;
            cAccount.Pages = account.Pages;
            cAccount.AdsAccountCount = account.AdsAccountCount;
            cAccount.BmCount = account.BmCount;
            cAccount.CreatedTime = account.CreatedTime;
            cAccount.Avatar = account.Avatar;
            cAccount.Cover = account.Cover;
            cAccount.Token = account.Token;
            cAccount.Language = account.Language;
            cAccount.Gender = account.Gender;
            cAccount.Notes = account.Notes;
            cAccount.AccountStatus = account.AccountStatus;
            cAccount.ModuleStatus = account.ModuleStatus;
            cAccount.AccountQuality = account.AccountQuality;
            cAccount.Location = account.Location;
            cAccount.LastUpdate = account.LastUpdate;
            cAccount.GroupCode = account.GroupCode;
            cAccount.UserAgent = account.UserAgent;

            cAccount.Proxy = account.Proxy;

            var result = SaveChanges();

            return result;
        }

        public ECodeInfo Delete(string u)
        {
            var account = Get(u);

            if (account == null)
            {
                return ECode.ACCOUNT_NOT_FOUND;
            }

            _context.Accounts.Remove(account);

            var result = SaveChanges();

            return result;
        }

        public Account Get(string u)
        {
            var account = _context.Accounts.Find(u);

            return account;
        }

        public List<Account> GetAll()
        {
            var list = _context.Accounts.ToList();

            return list;
        }

        public List<Account> GetByGroupCode(string groupCode)
        {
            var list = _context.Accounts.Where(t => t.GroupCode.Equals(groupCode)).ToList();

            return list;
        }

        public ECodeInfo AddIntoGroup(string groupCode, string u)
        {
            var account = Get(u);

            if (account == null)
            {
                return ECode.ACCOUNT_NOT_FOUND;
            }

            var group = new GroupDAO().Get(groupCode);

            if (group == null)
            {
                return ECode.GROUP_NOT_FOUND;
            }

            account.GroupCode = groupCode;

            var result = SaveChanges();

            return result;
        }

        public ECodeInfo DeleteFromGroup(string groupCode, string u)
        {
            var account = Get(u);

            if (account == null)
            {
                return ECode.ACCOUNT_NOT_FOUND;
            }

            var group = new GroupDAO().Get(groupCode);

            if (group == null)
            {
                return ECode.GROUP_NOT_FOUND;
            }

            account.GroupCode = null;

            var result = SaveChanges();

            return result;
        }
    }
}