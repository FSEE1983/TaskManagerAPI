using System.Collections.Generic;
using TaskManegerService.Models;

namespace TaskManegerService.Repository
{
    public interface ITaskRepository
    {
        void AddTask(Tasks TasksModel);
        void EndTask(int id);
        IEnumerable<Tasks> GetTask();
        Tasks GetTask(int id);
        void UpdateTask(int id, Tasks TasksModel);
    }
}