﻿using Microsoft.EntityFrameworkCore;
using OmnichannelDB.Persistence.Database;
using OmnichannelDB.Service.Queries.DTOs;
using OmnichannelDB.Service.Queries.Filters;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmnichannelDB.Service.Queries
{
    public interface IPlayerQueryService
    {
        Task<DataCollection<PlayerDto>> GetAllWithFilterAsync(FilterPlayers filterPlayers);
        Task<DataCollection<PlayerDto>> GetAllAsync(int page, int take, IEnumerable<int> products = null);
        Task<PlayerDto> GetAsync(int id);
    }

    public class PlayerQueryService : IPlayerQueryService
    {
        private readonly ApplicationDbContext _context;

        public PlayerQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<PlayerDto>> GetAllWithFilterAsync(FilterPlayers filterPlayers)
        {
            var collection = await _context.Players
                .Where(x => string.IsNullOrEmpty(filterPlayers.Username) || x.Username == filterPlayers.Username)
                .Where(x => string.IsNullOrEmpty(filterPlayers.Firstname) || x.Firstname == filterPlayers.Firstname)
                .Where(x => string.IsNullOrEmpty(filterPlayers.Lastname) || x.Lastname == filterPlayers.Lastname)
                .OrderByDescending(x => x.Id)
                .GetPagedAsync(filterPlayers.page, filterPlayers.take);

            return collection.MapTo<DataCollection<PlayerDto>>();
        }

        public async Task<DataCollection<PlayerDto>> GetAllAsync(int page, int take, IEnumerable<int> players = null)
        {
            var collection = await _context.Players
                .Where(x => players == null || players.Contains(x.Id))
                .OrderByDescending(x => x.Id)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<PlayerDto>>();
        }

        public async Task<PlayerDto> GetAsync(int id)
        {
           return (await _context.Players.SingleAsync(x => x.Id == id)).MapTo<PlayerDto>();
        }
    }
}
