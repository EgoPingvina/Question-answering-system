using System;
using System.IO;
using System.Text;

using VDS.RDF;
using VDS.RDF.Query;

namespace OntoMath_QAS.Ontology
{
    public sealed class RequestGenerator
    {
        private SparqlRemoteEndpoint endpoint = new SparqlRemoteEndpoint(new Uri("http://lobachevskii-dml.ru:8890/sparql"));

        public RequestGenerator()
        {
            Options.HttpDebugging = false;
        }

        public SparqlResultSet GetSet(string query)
            => this.endpoint.QueryWithResultSet(query);

        public IGraph GetGraph(string query)
        {
            this.endpoint.RdfAcceptHeader = "application/turtle";
            return endpoint.QueryWithResultGraph(query);
        }

        public string GetRaw(string query)
        {
            var result = new StringBuilder();

            using (var response = endpoint.QueryRaw(query))
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        result.AppendLine(
                            reader.ReadLine());
                    }

                    reader.Close();
                }

                response.Close();
            }

            return result.ToString();
        }
    }
}