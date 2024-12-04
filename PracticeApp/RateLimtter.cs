using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace AtlasianPrep.RateLimiter
{
    public class RateLimtter
    {
        //time window
        //max amount of tockens

        private TimeSpan _timeWindow;
        private int _maxTokens;

        private Dictionary<string, UserBucket> _users;

        public RateLimtter(TimeSpan timeWindow, int maxTokens)
        {
            _timeWindow = timeWindow;
            _maxTokens = maxTokens;
            _users = new Dictionary<string, UserBucket>();

        }

        public bool IsUserAllowed(string userId)
        {
            if (_users.TryGetValue(userId, out var user))
            {
                user.Refill();
                if (user.CurrentTokenCount <= 0)
                {
                    return false;
                }

                user.Decrement();

            }
            else
            {
                _users.Add(userId, new UserBucket(_maxTokens, _timeWindow));

            }


            return true;
        }

    }


    public class UserBucket
    {
        //lastrefill time 
        // current token count 

        private int _maxTokenCount;
        private TimeSpan _timeWindow;

        private DateTime _lastRefillTime;
        public int CurrentTokenCount { set; get; }

        public UserBucket(int maxTokenCount, TimeSpan timeWindow)
        {
            _maxTokenCount = maxTokenCount;
            _timeWindow = timeWindow;
            _lastRefillTime = DateAndTime.Now;
            CurrentTokenCount = _maxTokenCount;

        }


        public void Refill()
        {
            var now = DateTime.Now;
            var timeElapsed = now - _lastRefillTime;
            if (timeElapsed > _timeWindow)
            {
                CurrentTokenCount = _maxTokenCount;
                _lastRefillTime = DateAndTime.Now;

            }
        }

        public void Decrement()
        {
            if (CurrentTokenCount > 0)
            {
                CurrentTokenCount--;

            }

        }


    }
}