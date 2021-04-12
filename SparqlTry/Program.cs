using System;
using System.IO;
using System.Net;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Optimisation;
using VDS.RDF.Writing.Formatting;

namespace SparqlTry
{
    class Program
    {
		public static void Main(string[] args)
		{
            SparqlDBPedia();

            Console.ReadKey();

        }

        public static void SparqlDBPedia()
        {
            try
            {
                Options.HttpDebugging = true;

                var query =
                    @"
                        PREFIX dbpedia: <http://dbpedia.org/resource/>
                        PREFIX dbpedia-owl: <http://dbpedia.org/ontology/>
                        SELECT ?abstract
                        WHERE {
                            dbpedia:Junagadh dbpedia-owl:abstract ?abstract.
                            filter langMatches(lang(?abstract),""ru"")
                        }
                    ";
                var query2 =
                    @"
                        PREFIX dbo: <http://dbpedia.org/ontology/>
                        SELECT ?manufacturer ?name ?car
                        WHERE {
                            ?car <http://purl.org/dc/terms/subject> <http://dbpedia.org/resource/Category:Luxury_vehicles> .
                            ?car foaf:name ?name .
                            ?car dbo:manufacturer ?man .
                            ?man foaf:name ?manufacturer
                            filter langMatches(lang(?manufacturer),""en"")
                        }
                    ";
                
                var endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");

                var results = endpoint.QueryWithResultSet(query);

                endpoint.RdfAcceptHeader = "application/turtle";
                var graph = endpoint.QueryWithResultGraph(query);
                var ndoes = graph.AllNodes;

                using (var response = endpoint.QueryRaw(query))
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        while (!reader.EndOfStream)
                        {
                            Console.WriteLine(reader.ReadLine());
                        }
                        reader.Close();
                    }
                    response.Close();
                }

            }
            finally
            {
                Options.HttpDebugging = false;
            }
        }
    }
}