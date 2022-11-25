using Aurae_Facebook_Care.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurae_Facebook_Care.Models;

namespace Aurae_Facebook_Care.DAO
{
    public class GroupDAO : BaseDAO
    {
        public ECodeInfo Add(Group group)
        {
            var cGroup = Get(group.GroupCode);

            if (cGroup != null)
            {
                return ECode.GROUP_EXIST;
            }

            _context.Add(group);

            var result = SaveChanges();

            return result;
        }

        public ECodeInfo Update(Group group)
        {
            var cGroup = Get(group.GroupCode);

            if (cGroup == null)
            {
                return ECode.GROUP_NOT_FOUND;
            }

            cGroup.GroupName = group.GroupName;

            var result = SaveChanges();

            return result;
        }

        public ECodeInfo Delete(string groupCode)
        {
            var group = Get(groupCode);

            if (group == null)
            {
                return ECode.ACCOUNT_NOT_FOUND;
            }

            _context.Remove(group);

            var result = SaveChanges();

            return result;
        }

        public Group Get(string groupCode)
        {
            try
            {
                var group = _context.Groups.First(x => x.GroupCode == groupCode);

                return group;
            }
            catch
            {
                return null;
            }
        }

        public List<Group> GetByGroupName(string groupName)
        {
            try
            {
               List<Group> groups = _context.Groups.Where(x => x.GroupName.Contains(groupName)).ToList<Group>();

                return groups;
            }
            catch
            {
                return null;
            }
        }

        public List<Group> GetAll()
        {
            var list = _context.Groups.ToList();

            return list;
        }

    }
}