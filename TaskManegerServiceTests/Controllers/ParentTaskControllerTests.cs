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
    public class ParentTaskControllerTests
    {
        ParentTaskController controller;
        Mock<IParentTaskRepository> repository = new Mock<IParentTaskRepository>();
        List<ParentTasks> tasks = new List<ParentTasks>();

        [TestInitialize()]
        public void InitializeTest()
        {
            for (int i = 100; i < 500; i = i + 100)
            {
                tasks.Add(new ParentTasks { Parent_ID = i, Parent_Task = String.Format("Parent Task-{0}", i) });
            }

            repository.Setup(x => x.GetTask()).Returns(() => tasks);
            repository.Setup(x => x.GetTask(It.IsAny<int>())).Returns((int id)=> tasks.Where(x=>x.Parent_ID==id).FirstOrDefault());
            repository.Setup(x => x.AddTask(It.IsAny<ParentTasks>())).Callback((ParentTasks task) => tasks.Add(task));

            repository.Setup(x => x.UpdateTask(It.IsAny<int>(), It.IsAny<ParentTasks>())).Callback((int id, ParentTasks task) => {
                foreach (ParentTasks item in tasks)
                {
                    if (item.Parent_ID == id)
                    {
                        item.Parent_Task = task.Parent_Task;
                    }
                }
            });
            
            repository.Setup(x => x.DeleteTask(It.IsAny<int>())).Callback((int id) => tasks.RemoveAt(tasks.IndexOf(tasks.Where(x => x.Parent_ID == id).FirstOrDefault())));
                
            controller = new ParentTaskController(repository.Object);
        }
        [TestMethod()]
        public void GetTest()
        {
            IEnumerable<ParentTasks> task = controller.Get();
            Assert.IsNotNull(task, null);

        }

        [TestMethod()]
        public void GetTestById()
        {
            ParentTasks task = controller.Get(200);
            Assert.IsNotNull(task);
        }

        [TestMethod()]
        public void PostTest()
        {
            controller.Post(new ParentTasks { Parent_ID = 600, Parent_Task = "Test Task" });
            Assert.IsNotNull(controller.Get(600));
        }

        [TestMethod()]
        public void PutTest()
        {
            controller.Put(100, new ParentTasks { Parent_ID = 100, Parent_Task = "Test Task" });
            Assert.AreEqual(controller.Get(100).Parent_Task, "Test Task");
        }

        [TestMethod()]
        public void DeleteTest()
        {
            controller.Delete(200);
            Assert.IsNull(controller.Get(200));
        }
    }
}