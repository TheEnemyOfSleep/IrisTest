using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SensorCreators
{
    public class DatabaseSensorCreator : ISensorCreator
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSensorCreator(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateSensor(Sensor sensor)
        {
            using (MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SensorDTO sensorDTO = ToSensorDTO(sensor);

                context.Sensors.Add(sensorDTO);
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
