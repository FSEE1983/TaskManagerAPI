using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManegerService.Models;
using TaskManegerService.Repository;

namespace TaskManegerService.Controllers.Tests
{
    [TestClass()]
    public class TaskControllerTests
    {
        TaskController controller;
        Mock<ITaskRepository> repository = new Mock<ITaskRepository>();
        List<Tasks> tasks = new List<Tasks>();
        ParentTasks parentTask;

        [TestInitialize()]
        public void InitializeTest()
        {
            
            for (int i = 100; i < 500; i = i + 100)
            {
                parentTask = new ParentTasks { Parent_ID = 100, Parent_Task = "Parent Task" };
                for (int j = 1; j < 3; j++)
                {
                    tasks.Add(new Tasks { Parent_ID = i, ParentTask= parentTask, TaskName = String.Format("Task-{0}", i),Task_ID=i+j });
                }
               
            }

            repository.Setup(x => x.GetTask()).Returns(() => tasks);
            repository.Setup(x => x.GetTask(It.IsAny<int>())).Returns((int id)=>tasks.Find(x=>x.Task_ID==id));
            repository.Setup(x => x.AddTask(It.IsAny<Tasks>())).Callback((Tasks task) => tasks.Add(task));
            repository.Setup(x => x.UpdateTask(It.IsAny<int>(),It.IsAny<Tasks>())).Callback((int id,Tasks task)=>
                {
                    foreach (Tasks item in tasks)
                    {
                        if (item.Task_ID == id) { item.TaskName = task.TaskName; }
                    }
            });
           // repository.Setup(x => x.DeleteTask(It.IsAny<int>())).Callback((int id) => tasks.RemoveAt(tasks.IndexOf(tasks.Where(x => x.Task_ID == id).FirstOrDefault())));

            controller = new TaskController(repository.Object);
        }

        [TestMethod()]
        public void GetTest()
        {
            IEnumerable<Tasks> task = controller.Get();
            Assert.IsNotNull(task, null);
        }

        [TestMethod()]
        public void GetTest1()
        {
            Tasks task = controller.Get(101);
            Assert.IsNotNull(task);
        }

        [TestMethod()]
        public void PostTest()
        {
            controller.Post(new Tasks { Task_ID = 801, Parent_ID = 800, TaskName = "Task-800", ParentTask = new ParentTasks { Parent_ID = 800, Parent_Task = "Parent-800" } });
            Assert.IsNotNull(controller.Get(801));
        }

        [TestMethod()]
        public void PutTest()
        {
            controller.Put(201, new Tasks { Parent_ID = 200, ParentTask = parentTask, TaskName = String.Format("Task-U-{0}", 200), Task_ID = 201 });

            Assert.AreEqual(controller.Get(201).TaskName, String.Format("Task-U-{0}", 200));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            controller.Delete(101);

            Assert.IsNull(controller.Get(101));
        }
    }
}