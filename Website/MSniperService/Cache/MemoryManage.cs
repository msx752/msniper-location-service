using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Runtime.Caching;
using Newtonsoft.Json;
using MSniperService.Cache;
using MSniperService.Models;
using MSniperService.Statics;

public sealed class CacheManager<T> where T : class
{
    private static volatile CacheManager<T> instance;
    private static object syncRoot = new Object();
    private ObjectCache cache = null;
    private static CacheEntryRemovedCallback callback = null;

    private CacheManager()
    {
        cache = MemoryCache.Default;
        callback = new CacheEntryRemovedCallback(CachedItemRemovedCallback);

    }
    public static CacheManager<T> Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new CacheManager<T>();
                    }
                }
            }

            return instance;
        }
    }

    public List<T> GetAll()
    {
        var datas = new List<T>();
        foreach (var VARIABLE in cache)
        {
            var obj = VARIABLE.Value;
            if (obj is T)
            {
                datas.Add(obj as T);
            }
        }
        return datas;
    }

    public List<T> NonContainsCache(List<T> Keys)
    {
        var lst = new List<T>();
        foreach (var item in Keys)
        {
            object dat = null;
            if (typeof(T) == typeof(EncounterInfo))
            {
                dat = GetCache((item as EncounterInfo)?.UniqueKey());
                if (dat == null)
                {
                    AddCache(item as T);
                    lst.Add(item);
                }
            }
        }
        return lst;
    }

    public T GetCache(string Key)
    {
        if (Key == null)
        {
            return null;
        }

        try
        {
            string Key_ = Key;
            if (cache.Contains(Key_))
            {
                return (T)cache.Get(Key_);
            }
            else
            {
                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void AddRangeCache(List<T> data)
    {
        foreach (var item in data)
        {
            AddCache(item);
        }
    }

    public void AddCache(T data)
    {
        try
        {
            if (typeof(T) == typeof(EncounterInfo))
            {
                var Key_ = (data as EncounterInfo)?.UniqueKey();

                lock (Key_)
                {
                    if ((data as EncounterInfo)?.GetDateTime() > DateTime.Now.AddSeconds(-15) && (data as EncounterInfo).GetDateTime() < DateTime.Now.AddMinutes(25))
                    {
                        var cacheItemPolicy = new CacheItemPolicy();
                        cacheItemPolicy.AbsoluteExpiration = DateConverter.JavaTimeStampToDateTime((data as EncounterInfo).Expiration);
                        cacheItemPolicy.RemovedCallback = callback;
                        cache.Set(Key_, data, cacheItemPolicy);
                    }
                }
            }
            else if (typeof(T) == typeof(PokemonCounter))
            {
                var Key2_ = (data as PokemonCounter)?.UniqueKey();
                lock (Key2_)
                {
                    var cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMonths(12);
                    cacheItemPolicy.RemovedCallback = callback;
                    cache.Set(Key2_, data, cacheItemPolicy);
                }
            }
            else if (typeof(T) == typeof(RareList))
            {
                var Key3_ = (data as RareList)?.UniqueKey();
                lock (Key3_)
                {
                    var cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMonths(12);
                    cacheItemPolicy.RemovedCallback = callback;
                    cache.Set(Key3_, data, cacheItemPolicy);
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private static void CachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
    {
        //after deleting event
    }
}