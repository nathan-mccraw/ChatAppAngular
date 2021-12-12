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
    public class GetAllMessagesByChannelSpec : BaseSpecification<MessageEntity>
    {
        public GetAllMessagesByChannelSpec(string channelName)
            : base(m => m.Channel.ChannelName.ToLower() == channelName.ToLower())
        {
        }
    }
}