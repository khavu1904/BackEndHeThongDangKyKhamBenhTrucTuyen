using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDangKyKhamChuaBenh.Data;
using WebDangKyKhamChuaBenh.Models;

namespace WebDangKyKhamChuaBenh.Repositories
{
    public class PhongKhamRepository : IPhongKhamRepository
    { 
        private readonly MyAppDbContext _context;
        private readonly IMapper _mapper;

        public PhongKhamRepository(MyAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PhongKham>> GetAllPhongKhamAsync()
        {
            var phongKhamList = await _context.PhongKham.ToListAsync();
            return _mapper.Map<List<PhongKham>>(phongKhamList);
        }
    }
}
