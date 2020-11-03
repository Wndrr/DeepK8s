
window.startSimulation = (targetElement, nodes_data, links_data) =>
{
    var svg = d3.select(targetElement).attr("viewBox", `0 0 300 600`);

    var attractForce = d3.forceManyBody().strength(200).distanceMax(400).distanceMin(60);
    var repelForce = d3.forceManyBody().strength(-180).distanceMax(100).distanceMin(1);

    var simulation = d3.forceSimulation(nodes_data)
        .alphaDecay(0.03)
        .force("repelForce", repelForce)
        .force('center', d3.forceCenter(300 / 2, 600 / 2));

    var circle = svg.append("g")
        .attr("class", "nodes")
        .selectAll("circle")
        .data(nodes_data)
        .enter()
        .append("circle")
        .attr("class", function(d)
        {
            return "entity-circle-" + d.type;
        })
        .attr("r", 20);

    var text = svg.append("g")
        .attr("class", "labels")
        .selectAll("text")
        .data(nodes_data)
        .enter().append("text")
        .attr("dx", 20)
        .attr("dy", ".35em")
        .text(function (d)
        {
            return d.name
        });


    //add tick instructions: 
    simulation.on("tick", tickActions);


    //Time for the links 


    //Create the link force 
    //We need the id accessor to use named sources and targets 

    var link_force = d3.forceLink(links_data)
        .id(function (d)
        {
            return d.name;
        })

    //Add a links force to the simulation
    //Specify links  in d3.forceLink argument   


    simulation.force("links", link_force)

    //draw lines for the links 
    var link = svg.append("g")
        .attr("class", "links")
        .selectAll("line")
        .data(links_data)
        .enter().append("line")
        .attr("stroke-width", 2);

    function tickActions()
    {
        //update circle positions each tick of the simulation 
        circle
            .attr("cx", function (d)
            {
                return d.x;
            })
            .attr("cy", function (d)
            {
                return d.y;
            });

        text
            .attr("x", function (d)
            {
                return d.x;
            })
            .attr("y", function (d)
            {
                return d.y;
            });

        //update link positions 
        //simply tells one end of the line to follow one node around
        //and the other end of the line to follow the other node around
        link
            .attr("x1", function (d)
            {
                return d.source.x;
            })
            .attr("y1", function (d)
            {
                return d.source.y;
            })
            .attr("x2", function (d)
            {
                return d.target.x;
            })
            .attr("y2", function (d)
            {
                return d.target.y;
            });
    }

}
