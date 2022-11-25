using Aurae_Facebook_Care.Common;
using Aurae_Facebook_Care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurae_Facebook_Care.DAO
{
    public class ProfileDAO : BaseDAO
    {
        public ECodeInfo Add(Profile profile)
        {
            var account = new AccountDAO().Get(profile.U);

            if (account == null)
            {
                return ECode.ACCOUNT_NOT_FOUND;
            }

            var cProfile = Get(profile.U);

            if (cProfile != null)
            {
                return ECode.PROFILE_EXIST;
            }

            _context.Add(profile);

            var result = SaveChanges();

            return result;
        }

        public ECodeInfo Update(Profile profile)
        {
            var cProfile = Get(profile.U);

            if (cProfile == null)
            {
                return ECode.PROFILE_NOT_FOUND;
            }

            cProfile.Proxy = profile.Proxy;
            cProfile.UserAgent = profile.UserAgent;

            var result = SaveChanges();

            return result;
        }

        public ECodeInfo Delete(string u)
        {
            var cProfile = Get(u);

            if (cProfile == null)
            {
                return ECode.PROFILE_NOT_FOUND;
            }

            _context.Remove(cProfile);

            var result = SaveChanges();

            return result;
        }

        public Profile Get(string u)
        {
            var profile = _context.Profiles.Find(u);

            return profile;
        }

        public List<Profile> GetAll()
        {
            var list = _context.Profiles.ToList();

            return list;
        }
    }
}