﻿using HobbyListForHobbyist.Data;
using HobbyListForHobbyist.Models.DTOs;
using HobbyListForHobbyist.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbyListForHobbyist.Models.Services
{
    public class PaintService : IPaint
    {
        // ================== TODO ===========================
      private  HobbyListDbContext _context;

        public PaintService(HobbyListDbContext context)
        {
            _context = context;
        }
        // ================== TODO ===========================
        // CreateAPaint
        public async Task<PaintDTO> Create(PaintDTO paintdto)
        {
            Paint paint = new Paint()
            {
                ColorName = paintdto.ColorName,
                ProductNumber = paintdto.ProductNumber,
                Manufacturer = paintdto.Manufacturer
            };
                
            _context.Entry(paint).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return paintdto;
        }

        // GetAPaint
        public async Task<PaintDTO> GetPaint(int id)
        {
          Paint paint = await  _context.Paints.FindAsync(id);

            PaintDTO paintdto = new PaintDTO()
            {
                Id = id,
                ColorName = paint.ColorName,
                ProductNumber = paint.ProductNumber,
                Manufacturer = paint.Manufacturer
            };
            return paintdto;
        }

        // GetAllPaints
        public async Task<List<PaintDTO>> GetPaints()
        {
            var paint = await _context.Paints.ToListAsync();

            var paintdto = new List<PaintDTO>();

            foreach (var item in paint)
            {
                paintdto.Add(await GetPaint(item.Id));
            }

            return paintdto;
        }

        // UpdateAPaint
        public async Task<PaintDTO> Update( PaintDTO paintdto)
        {
            Paint paint = new Paint()
            {
                Id = paintdto.Id,
                ColorName = paintdto.ColorName,
                ProductNumber = paintdto.ProductNumber,
                Manufacturer = paintdto.Manufacturer
            };
            _context.Entry(paint).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return paintdto;
        }

        // DeleteAPaint
        public async Task Delete(int id)
        {
            Paint paint = await _context.Paints.FindAsync(id);

            _context.Entry(paint).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
