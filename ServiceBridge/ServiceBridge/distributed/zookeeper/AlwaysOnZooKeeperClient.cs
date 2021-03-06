﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using org.apache.zookeeper;
using ServiceBridge.extension;
using ServiceBridge.helper;
using ServiceBridge.data;
using ServiceBridge.core;
using System.Threading.Tasks;
using static org.apache.zookeeper.ZooDefs;
using org.apache.zookeeper.data;
using System.Net;
using System.Net.Http;
using ServiceBridge.rpc;
using ServiceBridge.distributed.zookeeper.watcher;

namespace ServiceBridge.distributed.zookeeper
{
    public class AlwaysOnZooKeeperClient : ZooKeeperClient
    {
        public event Action OnRecconected;

        public AlwaysOnZooKeeperClient(string host) : base(host)
        {
            this.OnUnConnected += () => this.ReConnect();
        }

        protected virtual void ReConnect()
        {
            //销毁的时候取消重试链接
            if (this.IsDisposing) { return; }

            this.CloseClient();
            this.CreateClient();
            this.OnRecconected.Invoke();
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
