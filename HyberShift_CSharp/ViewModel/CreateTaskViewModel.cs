﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using HyberShift_CSharp.Model;
using HyberShift_CSharp.Model.List;
using HyberShift_CSharp.Utilities;
using Prism.Commands;

namespace HyberShift_CSharp.ViewModel
{
    public class CreateTaskViewModel : BaseViewModel
    {
        private RoomModel currentRoom;
        private readonly ListTaskTypeModel listTaskTypeModel;
        private readonly TaskModel taskModel;

        public CreateTaskViewModel()
        {
            currentRoom = new RoomModel();
            listTaskTypeModel = new ListTaskTypeModel();
            taskModel = new TaskModel();
            taskModel.StartDay = DateTime.Now;
            taskModel.EndDay = DateTime.Now;
            CancelCommand = new DelegateCommand(Cancel);
            SubmitCommand = new DelegateCommand(CreateTask);
        }

        // getter and setter
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }
        public int SelectedIndexTag { get; set; }

        public RoomModel CurrentRoom
        {
            get => currentRoom;
            set
            {
                currentRoom = value;
                NotifyChanged("CurrentRoom");
            }
        }

        public ObservableCollection<string> ListMembers
        {
            get => currentRoom.Members;
            set
            {
                currentRoom.Members = value;
                NotifyChanged("ListMembers");
            }
        }

        public string Name
        {
            get => taskModel.Name;
            set
            {
                taskModel.Name = value;
                NotifyChanged("Name");
            }
        }

        public string Description
        {
            get => taskModel.Description;
            set
            {
                taskModel.Description = value;
                NotifyChanged("Description");
            }
        }

        public DateTime StartDay
        {
            get => taskModel.StartDay;
            set
            {
                taskModel.StartDay = value;
                NotifyChanged("StartDay");
            }
        }

        public DateTime EndDay
        {
            get => taskModel.EndDay;
            set
            {
                taskModel.EndDay = value;
                NotifyChanged("EndDay");
            }
        }

        public string Performer { get; set; }

        //{
        //    get { return taskModel.Performer; }
        //    set { taskModel.Performer = value; NotifyChanged("Performer"); }
        //}
        public ObservableCollection<TaskTypeModel> ListTag
        {
            get => listTaskTypeModel.List;
            set
            {
                listTaskTypeModel.List = value;
                NotifyChanged("ListTag");
            }
        }
        //public TaskType Tag
        //{
        //    get
        //    {
        //        return taskModel.Tag;
        //    }
        //    //set
        //    //{
        //    //    switch(SelectedIndexTag)
        //    //    {
        //    //        case 0:
        //    //            taskModel.Tag = TaskType.TO_DO;
        //    //            break;
        //    //        case 1:
        //    //            taskModel.Tag = TaskType.IN_PROGRESS;
        //    //            break;
        //    //        case 2:
        //    //            taskModel.Tag = TaskType.WARNING;
        //    //            break;
        //    //        case 3:
        //    //            taskModel.Tag = TaskType.DELAY;
        //    //            break;
        //    //        case 4:
        //    //            taskModel.Tag = TaskType.BACKLOG;
        //    //            break;
        //    //    }
        //    //    NotifyChanged("Tag");
        //    //}
        //}


        private void Cancel()
        {
            Debug.LogOutput(ListMembers[0]);
            foreach (Window window in Application.Current.Windows)
                if (window.Title == "CreateTaskDialog")
                    window.Close();
        }

        private void CreateTask()
        {
            //Debug.LogOutput(taskModel.Name + " " + taskModel.Description + " " + taskModel.Performer + " " + taskModel.StartDay + " " + taskModel.EndDay + " " + listTaskTypeModel.List[SelectedIndexTag].Content);
            taskModel.Tag = TaskTypeModel.GetTaskType(ListTag[SelectedIndexTag].Content);
            taskModel.Performer = Performer;

            taskModel.EmitToServer(CurrentRoom.ID);
            Cancel();
        }
    }
}