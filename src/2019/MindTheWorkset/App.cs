//GNU License
//Copyright(C) 2019 Parallax Team, Inc.
//This program is free software: you can redistribute it
//and/or modify it under the terms of the GNU General Public
//License as published by the Free Software Foundation, either
//version 3 of the License, or(at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the GNU
//General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.If not, see http://www.gnu.org/licenses/

#region Namespaces

using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;

#endregion

namespace MindTheWorkset
{
    class App : IExternalApplication
    {

        public Result OnStartup(UIControlledApplication a)
        {
            //event to remind us of the workset on synchronize.
            a.ControlledApplication.DocumentSynchronizedWithCentral += ControlledApplicationOnDocumentSynchronizedWithCentral;
            //event to remind us of the workset on document opening.
            a.ControlledApplication.DocumentOpened += ControlledApplicationOnDocumentOpened;
            return Result.Succeeded;
        }

        private void ControlledApplicationOnDocumentOpened(object sender, DocumentOpenedEventArgs e)
        {
            //on open, we need to see if it is workshared and show the dialog
            if (e.Document.IsWorkshared)
            {
                Document doc = e.Document;
                WorksetTable wt = doc.GetWorksetTable();
                string currentWorksetName = doc.GetWorksetTable().GetWorkset(wt.GetActiveWorksetId()).Name;
                MindTheWorksetDialog(doc, currentWorksetName);
            }
        }

        private void ControlledApplicationOnDocumentSynchronizedWithCentral(object sender, DocumentSynchronizedWithCentralEventArgs e)
        {
            Document doc = e.Document;
            WorksetTable wt = doc.GetWorksetTable();
            string currentWorksetName = doc.GetWorksetTable().GetWorkset(wt.GetActiveWorksetId()).Name;
            MindTheWorksetDialog(doc,currentWorksetName);
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            //unsubscribe from event to remind us of the workset on synchronize.
            a.ControlledApplication.DocumentSynchronizedWithCentral -= ControlledApplicationOnDocumentSynchronizedWithCentral;
            //unsubscribe from event to remind us of the workset on document opening.
            a.ControlledApplication.DocumentOpened -= ControlledApplicationOnDocumentOpened;
            return Result.Succeeded;
        }

        //we use a method for this to reduce redundancy
        private void MindTheWorksetDialog(Document doc,string currentWorksetName)
        {
            UIApplication uiapp = new UIApplication(doc.Application);
            //construct a new task dialog
            TaskDialog td = new TaskDialog("Mind The Workset - App Idea by @BIMchiq");
            //removes the addin id from the dialog
            td.TitleAutoPrefix = false;
            //add all the cool stuff to the task dialog
            td.MainContent = "This is to remind you that your current workset is, 👉 " + currentWorksetName;
            td.AddCommandLink(TaskDialogCommandLinkId.CommandLink1,"Cool, I am happy with using, 👉 " + currentWorksetName +". 😎");
            td.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "Yikes, I need to change this. 😱 Take me to the worksets dialog please.");
            //show the thing!
            TaskDialogResult tResult = td.Show();
            if (tResult == TaskDialogResult.CommandLink1)
            {
                //do nothing if they are wanting to continue
            }
            else if (tResult == TaskDialogResult.CommandLink2)
            { 
                //open the workset dialog if they need to change their mind
                RevitCommandId cmdId = RevitCommandId.LookupPostableCommandId(PostableCommand.Worksets);
                if (uiapp.CanPostCommand(cmdId))
                    uiapp.PostCommand(cmdId);
            }
        }
    }
}
