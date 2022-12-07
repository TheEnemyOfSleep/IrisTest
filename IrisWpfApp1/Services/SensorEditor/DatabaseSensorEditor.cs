using IrisWpfApp1.DbContexts;
using IrisWpfApp1.DTOs;
using IrisWpfApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisWpfApp1.Services.SensorEditor
{
    public class DatabaseSensorEditor: ISensorEditor
    {
        private readonly MainDbContextFactory _dbContextFactory;

        public DatabaseSensorEditor(MainDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task EditSensor(Sensor sensor)
        {
            using (DbContexts.MainDbContext context = _dbContextFactory.CreateDbContext())
            {
                SensorDTO sensorDTO = ToSensorDTO(sensor);

                var sensorDb = await context.Sensors.FirstOrDefaultAsync(v => v.SensorNumber == sensorDTO.SensorNumber);
                sensorDb.Location = sensorDTO.Location;
                sensorDb.Charge = sensorDTO.Charge;
                sensorDb.SensorStateId = sensorDTO.SensorStateId;
                context.Entry<SensorDTO>(sensorDb).State = EntityState.Modified;

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
