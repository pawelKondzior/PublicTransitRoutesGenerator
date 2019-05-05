namespace Magisterka.Data.Access.PP
{
    using System;



    public partial class Parameter : DBContextDB.Record<Parameter>
    {


        public Parameter CloneParameterValues()
        {
            var newItem = new Parameter();

            newItem.TestAlgorithmTypeEnum = this.TestAlgorithmTypeEnum;
            newItem.AdaptationFunctionTypeEnum = this.AdaptationFunctionTypeEnum;
            newItem.ChangeNumber = this.ChangeNumber;
            newItem.LinkType = this.LinkType;
            newItem.PopulationCount = this.PopulationCount;
            newItem.MutationProbability = this.MutationProbability;
            newItem.NumberOfEvaluation = this.NumberOfEvaluation;
            newItem.NumberOfSquares = this.NumberOfSquares;
            newItem.NumberOfNeighborSquares = this.NumberOfNeighborSquares;

            return newItem;
        }
    }
}
