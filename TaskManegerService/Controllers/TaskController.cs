using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using TaskManegerService.Models;
using TaskManegerService.Repository;

namespace TaskManegerService.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public class TaskController : ApiController
    {
        ITaskRepository taskRepository;

        public TaskController():this(new TaskRepository())
        {

        }
        public TaskController(ITaskRepository _taskRepository)
        {
            taskRepository = _taskRepository;
        }
        // GET: api/Task
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<Tasks> Get()
        {
           return taskRepository.GetTask();
        }

        // GET: api/Task/5
        [HttpGet]
        [AllowAnonymous]
        public Tasks Get(int id)
        {
            return taskRepository.GetTask(id);
        }

        // POST: api/Task
        [HttpPost]
        [AllowAnonymous]
        public void Post(Tasks TasksModel)
        {
            taskRepository.AddTask(TasksModel);
        }

        // PUT: api/Task/5
        [HttpPut]
        [AllowAnonymous]
        public void Put(int id, Tasks TasksModel)
        {
            taskRepository.UpdateTask(id, TasksModel);
        }

        // DELETE: api/Task/5
        [HttpDelete]
        [AllowAnonymous]
        public void Delete(int id)
        {
            taskRepository.EndTask(id);
        }
    }
}
