﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GitHub.Models;
using GitHub.Repositories;

namespace GitHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }

        public IAttendanceRepository Attendees { get; private set; }

        public IFollowingRepository Followings { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendees = new AttendanceRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres= new GenreRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}