using eGovernmernt_Service.Models;
using eGovernmernt_Service.Services;
using Microsoft.EntityFrameworkCore;

namespace eGovernmernt_Service
{
    public class DataService
    {
        private readonly ApplicationContext _context;

        public DataService(ApplicationContext context)
        {
            _context = context;
        }

        public Task<List<TrafficRegistrationApplication>> GetTrafficRegistrationApplicationsAsync()
        {
            return _context.TrafficRegistrationApplication.ToListAsync();
        }

        public Task<List<TrafficLicenceApplication>> GetTrafficLicenceApplicationsAsync()
        {
            return _context.TrafficLicenceApplication.ToListAsync();
        }
    }
}
