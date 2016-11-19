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
    private static readonly object syncRoot = new Object();
    private readonly ObjectCache cache = null;
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
            if (typeof(T) == typeof(EncounterInfo))
            {
                object dat = GetCache((item as EncounterInfo)?.UniqueKey());
                if (dat == null)
                {
                    if (CheckAddState(item as EncounterInfo))
                    {
                        AddCache(item as T);
                        lst.Add(item);
                    }
                }
            }
        }
        return lst;
    }

    public bool CheckAddState(EncounterInfo item)
    {
        try
        {
            PokemonId poid = (PokemonId)Enum.Parse(typeof(PokemonId), item.PokemonName);
            PokemonGrades poGrade = PokemonGradeHelper.GetPokemonGrade(poid);
            if (poGrade == PokemonGrades.VeryCommon)
                return item.Iv >= 65;
            else if (poGrade == PokemonGrades.Common)
                return item.Iv >= 55;
            else if (poGrade == PokemonGrades.Popular)
                return item.Iv >= 35;
            else if (poGrade == PokemonGrades.Rare)
                return item.Iv >= 15;
            else if (poGrade == PokemonGrades.Popular)
                return item.Iv >= 0;
            else if (poGrade == PokemonGrades.Epic)
                return item.Iv >= 0;
            else if (poGrade == PokemonGrades.Legendary)
                return item.Iv >= 0;
            else if (poGrade == PokemonGrades.NONE)
                return item.Iv >= 0;
        }
        catch (Exception)
        {
            return false;
        }
        return false;
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
                var key_ = (data as EncounterInfo)?.UniqueKey();

                lock (key_)
                {
                    if (!((data as EncounterInfo)?.GetDateTime() > DateTime.Now.AddSeconds(-5)) ||
                        (data as EncounterInfo).GetDateTime() >= DateTime.Now.AddMinutes(25)) return;

                    var cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateConverter.JavaTimeStampToDateTime((data as EncounterInfo).Expiration);
                    cacheItemPolicy.RemovedCallback = callback;
                    cache.Set(key_, data, cacheItemPolicy);
                }
            }
            else if (typeof(T) == typeof(PokemonCounter))
            {
                var key2 = (data as PokemonCounter)?.UniqueKey();
                lock (key2)
                {
                    var cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMonths(12);
                    cacheItemPolicy.RemovedCallback = callback;
                    cache.Set(key2, data, cacheItemPolicy);
                }
            }
            else if (typeof(T) == typeof(RareList))
            {
                var key3 = (data as RareList)?.UniqueKey();
                lock (key3)
                {
                    var cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMonths(12);
                    cacheItemPolicy.RemovedCallback = callback;
                    cache.Set(key3, data, cacheItemPolicy);
                }
            }
        }
        catch (Exception e)
        {

        }
    }

    private static void CachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
    {
        //after deleting event
    }
}