// -----------------------------------------------------------------------
//  <copyright file="LineCombinationItem.cs" company="DevCore .NET">
//      Copyright (c) DevCore.NET All rights reserved.
//  </copyright>
//  <author>Paweł Kondzior</author>
// -----------------------------------------------------------------------


namespace Magisterka.Infrastructure.Shared.Structure
{
    using Magisterka.Infrastructure.Shared.IoDto;
    using System.Collections.Generic;
    using System.Linq;


    public class LineCombinationList : List<LineCombination>
    {
        
    }

    public class LineCombination
    {
        public List<LineCombinationItem> LineCombinationItems { get; set; }

        public bool ValidLineCombination { get; set; }

        public LineCombination(List<PartOfBusLine> partOfBusLines, List<WalkLineChanges> walkLineChanges, BusStop startDestination, BusStop endDestination)
        {
            ValidLineCombination = true;
            LineCombinationItems = new List<LineCombinationItem>();

            var previous = partOfBusLines.FirstOrDefault();
            var first = previous;

            LineCombinationItems.Add(new LineCombinationItem(previous));

            if (previous.Start == startDestination && endDestination == previous.End)
            {
                ValidLineCombination = true;
                return;
            }

            foreach (var next in partOfBusLines.Skip(1))
            {
                if (first.Start == startDestination && next.End == endDestination)
                {
                    ValidLineCombination = true;
                    return;
                }

                if (previous.End == next.Start)
                {
                    LineCombinationItems.Add(new LineCombinationItem(next));
                }
                else
                {
                    var connectingWalkLine = previous.GetConnectingWalkLine(next, walkLineChanges);

                    if (connectingWalkLine == null)
                    {
                        ValidLineCombination = false;
                        return; 
                    }
                    else
                    {
                        LineCombinationItems.Add(new LineCombinationItem(connectingWalkLine));
                        LineCombinationItems.Add(new LineCombinationItem(next));
                    }    
                }
                
                previous = next;
            }
        }

        public LineCombination()
        {
            ValidLineCombination = true;
            LineCombinationItems = new List<LineCombinationItem>();
         }
    }

    public class LineCombinationItem
    {
        public PartOfBusLine PartOfBusLine { get; set; }

        public WalkLineChanges WalkLineChanges { get; set; }

        public LineCombinationItem(PartOfBusLine partOfBusLine)
        {
            this.PartOfBusLine = partOfBusLine;
        }

        public LineCombinationItem(WalkLineChanges walkLineChanges)
        {
            this.WalkLineChanges = walkLineChanges;
        }

    }
}