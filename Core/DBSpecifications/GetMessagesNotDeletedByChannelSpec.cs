using Core.Entities;
using Core.DBSpecifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DBSpecifications
{
    public class GetMessagesNotDeletedByChannelSpec : BaseSpecification<MessageEntity>
    {
        public GetMessagesNotDeletedByChannelSpec(string channelName)
            : base(
                    m => m.Channel.ChannelName.ToLower() == channelName.ToLower() &&
                    m.DateDeleted == null
                  )
        {
        }
    }
}