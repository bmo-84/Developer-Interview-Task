using InterviewTask.Models;
using System.Collections.Generic;

namespace InterviewTask.Services
{
    public interface IHelperServiceMapper
    {
        IEnumerable<HelperServiceViewModel> MapToViewModel(IEnumerable<HelperServiceModel> helperServices);
    }
}