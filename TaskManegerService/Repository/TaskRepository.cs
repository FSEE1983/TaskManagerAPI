using System.Collections.Generic;
using System.Linq;
using TaskManegerService.Models;
namespace TaskManegerService.Repository
{
    public enum enumParentTaskType
    {
        None,
        Parent,
        Main
    }
    public class TaskRepository : ITaskRepository
    {
      
        public IEnumerable<Tasks> GetTask()
        {
            var listOfTasks = new List<Tasks>();
            var MainTaskList = new TaskDBEntities().Tasks;
            var ParentTaskList = new TaskDBEntities().ParentTasks;

            Tasks objTask = null;
            foreach (var task in MainTaskList)
            {
                objTask = new Tasks()
                {
                    Task_ID = task.Task_ID,
                    TaskName = task.Task1,
                    Start_Date = task.Start_Date,
                    End_Date = task.End_Date,
                    Priority = task.Priority,
                    ParentTaskType = task.ParentTaskType,
                    Parent_ID = task.Parent_ID,
                    IsTaskEnded = task.IsTaskEnded
                };

                if (task.ParentTaskType == enumParentTaskType.Parent.ToString())
                {
                    var pTask = ParentTaskList.FirstOrDefault(e => e.Parent_ID == task.Parent_ID);
                    if (pTask != null)
                    {
                        objTask.ParentTask = new ParentTasks()
                        {
                            Parent_ID = pTask.Parent_ID,
                            Parent_Task = pTask.Parent_Task
                        };
                    }
                }
                else if (task.ParentTaskType == enumParentTaskType.Main.ToString())
                {
                    var MainTask = MainTaskList.FirstOrDefault(e => e.Task_ID == task.Parent_ID);
                    if (MainTask != null)
                    {
                        objTask.ParentTask = new ParentTasks()
                        {
                            Parent_ID = MainTask.Task_ID,
                            Parent_Task = MainTask.Task1
                        };
                    }
                }
                listOfTasks.Add(objTask);
            }
            return listOfTasks;
        }

         
        public Tasks GetTask(int id)
        {
            Tasks objTask = null;
            var task = new TaskDBEntities().Tasks.FirstOrDefault(x => x.Task_ID == id);
            if (task != null)
            {
                objTask = new Tasks()
                {
                    Task_ID = task.Task_ID,
                    TaskName = task.Task1,
                    Start_Date = task.Start_Date,
                    End_Date = task.End_Date,
                    Priority = task.Priority,
                    Parent_ID = task.Parent_ID,
                    ParentTaskType = task.ParentTaskType,
                    IsTaskEnded = task.IsTaskEnded
                };

                if (task.ParentTaskType == enumParentTaskType.Parent.ToString())
                {
                    var pTask = new TaskDBEntities().ParentTasks.FirstOrDefault(e => e.Parent_ID == task.Parent_ID);
                    if (pTask != null)
                    {
                        objTask.ParentTask = new ParentTasks()
                        {
                            Parent_ID = pTask.Parent_ID,
                            Parent_Task = pTask.Parent_Task,
                            ParentTaskType = enumParentTaskType.Parent.ToString()
                        };
                    }
                }
                else if (task.ParentTaskType == enumParentTaskType.Main.ToString())
                {
                    var MainTask = new TaskDBEntities().Tasks.FirstOrDefault(e => e.Task_ID == task.Parent_ID);
                    if (MainTask != null)
                    {
                        objTask.ParentTask = new ParentTasks()
                        {
                            Parent_ID = MainTask.Task_ID,
                            Parent_Task = MainTask.Task1,
                            ParentTaskType = enumParentTaskType.Main.ToString()
                        };
                    }
                }
            }
            return objTask;
        }

        
        public void AddTask(Tasks TasksModel)
        {
            var TaskDBcontext = new TaskDBEntities();
            var task = new Task();
            task.Task_ID = TasksModel.Task_ID;
            task.Task1 = TasksModel.TaskName;
            task.Start_Date = TasksModel.Start_Date;
            task.End_Date = TasksModel.End_Date;
            task.Priority = TasksModel.Priority;
            task.Parent_ID = TasksModel.Parent_ID;
            task.ParentTaskType = TasksModel.ParentTaskType;
            task.IsTaskEnded = TasksModel.IsTaskEnded;
            TaskDBcontext.Tasks.Add(task);
            TaskDBcontext.SaveChanges();
        }

       
        public void UpdateTask(int id, Tasks TasksModel)
        {
            var TaskDBcontext = new TaskDBEntities();
            var task = TaskDBcontext.Tasks.FirstOrDefault(x => x.Task_ID == id); 
            if (task != null)
            {
                task.Task_ID = TasksModel.Task_ID;
                task.Task1 = TasksModel.TaskName;
                task.Start_Date = TasksModel.Start_Date;
                task.End_Date = TasksModel.End_Date;
                task.Priority = TasksModel.Priority;
                task.Parent_ID = TasksModel.Parent_ID;
                task.ParentTaskType = TasksModel.ParentTaskType;
                task.IsTaskEnded = TasksModel.IsTaskEnded;
                TaskDBcontext.SaveChanges();
            }
        }

         
        public void EndTask(int id)
        {
            var TaskDBcontext = new TaskDBEntities();
            var task = TaskDBcontext.Tasks.FirstOrDefault(x => x.Task_ID == id);
            if (task != null)
            {               
                task.IsTaskEnded = 1;
                TaskDBcontext.SaveChanges();
            }
        }
    }
}