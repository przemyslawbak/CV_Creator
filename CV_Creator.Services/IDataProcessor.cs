﻿using System.Collections.Generic;
using CV_Creator.Models;

namespace CV_Creator.Services
{
    public interface IDataProcessor
    {
        byte[] ProcessPortfolio(List<Project> loadedProjects, List<Technology> loadedTechStack, string companyName, string positionApplied);
    }
}