﻿/****************************************************************************
Copyright (c) 2013-2015 scutgame.com

http://www.scutgame.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
****************************************************************************/
using System;
using ZyGames.Framework.Common.Configuration;
using ZyGames.Framework.Common.Security;
using ZyGames.Framework.Data;
using ZyGames.Framework.Game.Runtime;

namespace ZyGames.Framework.Game.Pay
{
    internal class ConfigManger
    {
        private static readonly DbBaseProvider _dbBaseProvider;

        static ConfigManger()
        {
            string providerType = ConfigUtils.GetSetting("PayDB_ProviderType");
            string connectionFormat = ConfigUtils.GetSetting("PayDB_ConnectionString");
            string dataSource = string.Empty;
            string userInfo = string.Empty;
            try
            {
                dataSource = ConfigUtils.GetSetting("PayDB_Server");
                userInfo = ConfigUtils.GetSetting("PayDB_Acount");
                if (!string.IsNullOrEmpty(userInfo))
                {
                    userInfo = CryptoHelper.DES_Decrypt(userInfo, GameEnvironment.ProductDesEnKey);
                }
            }
            catch (Exception)
            {
            }
            string connectionString = "";
            if (!string.IsNullOrEmpty(dataSource) && !string.IsNullOrEmpty(userInfo))
            {
                connectionString = string.Format(connectionFormat, dataSource, userInfo);
            }
            _dbBaseProvider = DbConnectionProvider.CreateDbProvider("PayDB", providerType, connectionString);
        }

        public static DbBaseProvider Provider
        {
            get { return _dbBaseProvider; }
        }
    }
}