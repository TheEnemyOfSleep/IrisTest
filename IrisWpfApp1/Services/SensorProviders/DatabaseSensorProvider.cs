using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SensorProviders
{
    public class DatabaseSensorProvider: ISensorProvider
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSensorProvider(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Sensor>> GetAllSensors()
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SensorDTO> sensorsDTOs = await context.Sensors.ToListAsync();

                return sensorsDTOs.Select(v => new Sensor(v.SensorNumber, v.Location, v.Charge, v.SensorStateId));
            }
        }

        public async Task<Sensor> GetSensorById(Sensor sensor)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SensorDTO> sensorsDTOs = await context.Sensors.ToListAsync();
                SensorDTO sensorDTO = sensorsDTOs.Where(v => v.SensorNumber == sensor.SensorNumber).FirstOrDefault();

                return new Sensor(sensorDTO.SensorNumber, sensorDTO.Location, sensorDTO.Charge, sensorDTO.SensorStateId);
            }
        }

        public async Task<Sensor> GetSensorBySensorNumber(string sensorNumber)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<SensorDTO> sensorsDTOs = await context.Sensors.ToListAsync();
                SensorDTO sensorDTO = sensorsDTOs.Where(v => v.SensorNumber == sensorNumber).FirstOrDefault();

                return new Sensor(sensorDTO.SensorNumber, sensorDTO.Location, sensorDTO.Charge, sensorDTO.SensorStateId);
            }
        }
    }
}
