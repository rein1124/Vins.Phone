﻿using System;
using System.Reactive;

namespace ODM.Domain.Inspection
{
    public interface IInspectService
    {
        IObservable<int> AcquisitionStartedEvent { get; }

        IObservable<ImageInfo> AcquisitionCompletedEvent { get; }

        IObservable<int> InspectionStartedEvent { get; }

        IObservable<SurfaceInspectInfo> InspectionCompletedEvent { get; }

        void Start();

        void Stop();

        void Reset();

        void InspectImageFile(int surfaceTypeIndex,  string fileName);
    }
}