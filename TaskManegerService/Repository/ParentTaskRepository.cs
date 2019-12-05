using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManegerService.Models;

namespace TaskManegerService.Repository
{
    public class ParentTaskRepository : IParentTaskRepository
    {
        public IEnumerable<ParentTasks> GetTask()
        {
            var listOfTasks = new List<ParentTasks>();
            var ParentTasklist = new TaskDBEntities().ParentTasks;
            var MainTasklist = new TaskDBEntities().Tasks;
            foreach (var task in ParentTasklist)
            {
                listOfTasks.Add(new ParentTasks()
                {
                    Parent_ID = task.Parent_ID,
                    Parent_Task = task.Parent_Task,
                    ParentTaskType = enumParentTaskType.Parent.ToString()
                });
            };            
            foreach (var task in MainTasklist)
            {
                listOfTasks.Add(new ParentTasks()
                {
                    Parent_ID = task.Task_ID,
                    Parent_Task = task.Task1,
                    ParentTaskType = enumParentTaskType.Main.ToString()
                });
            }
            return listOfTasks;
        }


        public ParentTasks GetTask(int id)
        {
            ParentTasks Tasks = null;
            var task = new TaskDBEntities().ParentTasks.FirstOrDefault(x => x.Parent_ID == id);
            if (task != null)
            {
                Tasks = new ParentTasks()
                {
                    Parent_ID = task.Parent_ID,
                    Parent_Task = task.Parent_Task
                };
            }
            return Tasks;
        }


        public void AddTask(ParentTasks TasksModel)
        {
            var TaskDBcontext = new TaskDBEntities();
            var task = new ParentTask();
            task.Parent_ID = TasksModel.Parent_ID;
            task.Parent_Task = TasksModel.Parent_Task; 
            TaskDBcontext.ParentTasks.Add(task);
            TaskDBcontext.SaveChanges();
        }


        public void UpdateTask(int id, ParentTasks TasksModel)
        {
            var TaskDBcontext = new TaskDBEntities();
            var task = TaskDBcontext.ParentTasks.FirstOrDefault(x => x.Parent_ID == id);
            if (task != null)
            {
                task.Parent_ID = TasksModel.Parent_ID;
                task.Parent_Task = TasksModel.Parent_Task; 
                TaskDBcontext.SaveChanges();
            }
        }


        public void DeleteTask(int id)
        {
            var TaskDBcontext = new TaskDBEntities();
            var task = TaskDBcontext.ParentTasks.FirstOrDefault(x => x.Parent_ID == id);
            if (task != null)
            {
                TaskDBcontext.ParentTasks.Remove(task);
                TaskDBcontext.SaveChanges();
            }
        }
    }
}