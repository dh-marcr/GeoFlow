/*

make a cube follow a path;

setup triggers for entering tile
	-make a script on the cube
	-Have it contain the cube’s path
	-overtime you enter a tile set a new path for tile

	how to travel on that tile
		-new function on tiles
		-assign entry point trigger
		-call the “new path” function on tile
		-this is containing the path created by the level designer

	figure out settings for each tile
		-position
		-max weight
		-startWeight
			-entry point
			-exit point	
			-path(array of vector3 or transform)

	**figure out over the course of development**
		-all infö that will get stored in JSON for creation
			-level config class
				-level manager config class
				-tile config class(all tiles)
					-evironment config class(all tiles)
					
*/