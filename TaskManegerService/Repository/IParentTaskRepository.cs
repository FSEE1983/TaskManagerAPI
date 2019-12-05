using System.Collections.Generic;
using TaskManegerService.Models;

namespace TaskManegerService.Repository
{
    public interface IParentTaskRepository
    {
        void AddTask(ParentTasks TasksModel);
        void DeleteTask(int id);
        IEnumerable<ParentTasks> GetTask();
        ParentTasks GetTask(int id);
        void UpdateTask(int id, ParentTasks TasksModel);
    }
}