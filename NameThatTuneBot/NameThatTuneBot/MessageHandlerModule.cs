using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace NameThatTuneBot
{
    public abstract class MessageHandlerModule
    {
        protected IBotMediator botMediator;

        internal void AddMediator(IBotMediator botMediator)
        {
            this.botMediator = botMediator;
        }

        protected abstract Task Send(Message message);
        public abstract Task Receive(Message message);

    }
}
