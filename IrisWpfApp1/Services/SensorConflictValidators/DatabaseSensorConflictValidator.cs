using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SensorConflictValidators
{
    public class DatabaseSensorConflictValidator: ISensorConflictValidator
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSensorConflictValidator(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Sensor> GetConflictingSensor(Sensor sensor)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SensorDTO sensorDTO = await context.Sensors.Where(v => v.SensorNumber == sensor.SensorNumber).FirstOrDefaultAsync();
                if (sensorDTO == null)
                {
                    return null;
                }

                return ToSensors(sensorDTO);
            }
        }

        public async Task<Sensor> GetConflictingSensor(string sensorNumber)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SensorDTO sensorDTO = await context.Sensors.Where(v => v.SensorNumber == sensorNumber).FirstOrDefaultAsync();
                if (sensorDTO == null)
                {
                    return null;
                }

                return ToSensors(sensorDTO);
            }
        }

        private static Sensor ToSensors(SensorDTO dto)
        {
            return new Sensor(dto.SensorNumber, dto.Location, dto.Charge, dto.SensorStateId);
        }
    }
}
