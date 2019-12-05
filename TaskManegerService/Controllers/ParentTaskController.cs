using System.Collections.Generic;
using System.Web.Http;
using TaskManegerService.Models;
using TaskManegerService.Repository;

namespace TaskManegerService.Controllers
{
    public class ParentTaskController : ApiController
    {
        IParentTaskRepository parentTaskRepository;
        public ParentTaskController() : this (new ParentTaskRepository())
        {

        }

        public ParentTaskController(IParentTaskRepository _parentTaskRepository)
        {
            parentTaskRepository = _parentTaskRepository;
        }
        // GET: api/Task
        public IEnumerable<ParentTasks> Get()
        {
            return parentTaskRepository.GetTask();
        }

        // GET: api/Task/5
        public ParentTasks Get(int id)
        {
            return parentTaskRepository.GetTask(id);
        }

        // POST: api/Task
        public void Post(ParentTasks TasksModel)
        {
            parentTaskRepository.AddTask(TasksModel);
        }

        // PUT: api/Task/5
        public void Put(int id, ParentTasks TasksModel)
        {
            parentTaskRepository.UpdateTask(id, TasksModel);
        }

        // DELETE: api/Task/5
        public void Delete(int id)
        {
            parentTaskRepository.DeleteTask(id);
        }
    }
}
