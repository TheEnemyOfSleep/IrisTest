using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SensorRemover
{
    public class DatabaseSensorRemover: ISensorRemover
    {

        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSensorRemover(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task RemoveSensor(Sensor sensor)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SensorDTO sensorDTO = ToSensorDTO(sensor);

                context.Sensors.Remove(sensorDTO);
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveSensor(string sensorNumber)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SensorDTO sensorDTO = await context.Sensors.FirstOrDefaultAsync(v => v.SensorNumber == sensorNumber);

                context.Sensors.Remove(sensorDTO);
                await context.SaveChangesAsync();
            }
        }


        private SensorDTO ToSensorDTO(Sensor sensor)
        {
            return new SensorDTO()
            {
                SensorNumber = sensor.SensorNumber,
                Location = sensor.Location,
                Charge = sensor.Charge,
                SensorStateId = sensor.SensorStateId
            };
        }
    }
}
