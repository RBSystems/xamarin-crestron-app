﻿using CrestronApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(CrestronApp.Services.CrestronDataStore))]
namespace CrestronApp.Services
{
    public class CrestronDataStore : IDataStore<CrestronRoom>
    {
        bool isInitialized;
        List<CrestronRoom> crestronRooms;

        public async Task<bool> AddItemAsync(CrestronRoom CrestronRoom)
        {
            await InitializeAsync();
            crestronRooms.Add(CrestronRoom);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(CrestronRoom CrestronRoom)
        {
            await InitializeAsync();
            var _CrestronRoom = crestronRooms.Where((CrestronRoom arg) => arg.Id == CrestronRoom.Id).FirstOrDefault();
            crestronRooms.Remove(_CrestronRoom);
            crestronRooms.Add(CrestronRoom);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(CrestronRoom CrestronRoom)
        {
            await InitializeAsync();
            var _CrestronRoom = crestronRooms.Where((CrestronRoom arg) => arg.Id == CrestronRoom.Id).FirstOrDefault();
            crestronRooms.Remove(_CrestronRoom);
            return await Task.FromResult(true);
        }

        public async Task<CrestronRoom> GetItemAsync(string id)
        {
            await InitializeAsync();
            return await Task.FromResult(crestronRooms.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<CrestronRoom>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();
            return await Task.FromResult(crestronRooms);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            crestronRooms = new List<CrestronRoom>();
            var _crestronRooms = new List<CrestronRoom>
            {
                new CrestronRoom { Id = Guid.NewGuid().ToString(), Name ="Test room",IpAddress="127.0.0.1"},
                   };

            foreach (CrestronRoom CrestronRoom in _crestronRooms)
            {
                crestronRooms.Add(CrestronRoom);
            }

            isInitialized = true;
        }
    }
}
