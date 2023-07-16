import React, { useEffect, useState } from "react";
import { Movie, MoviesResponse } from "../api/api";
import { MovieDetail } from "./MovieDetails";
import { DefaultButton, Stack, StackItem } from "@fluentui/react";

export function MovieList() {
  const [state, setState] = useState<ComponentState>({
    data: [],
    loading: true,
  });
  const [selectedMovie, setSelectedMovie] = useState<string | undefined>(
    undefined,
  );

  useEffect(() => {
    fetchMovies(setState);
  }, []);

  return (
    <Stack>
      <StackItem>
        <table className="table table-striped" aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>ID</th>
              <th>Movie</th>
              <th>Number of Characters</th>
              <th>Number of Quotes</th>
            </tr>
          </thead>
          <tbody>
            {state.data.map((movie) => (
              <tr key={movie.id}>
                <td>{movie.id}</td>
                <td>
                  <DefaultButton onClick={() => setSelectedMovie(movie.id)}>
                    {movie.name}{" "}
                  </DefaultButton>
                </td>
                <td>{movie.characters?.length}</td>
                <td>{movie.quotes?.length}</td>
                <td>
                  <DefaultButton
                    onClick={() => {
                      if (movie.id === selectedMovie) {
                        setSelectedMovie(undefined);
                      }
                      deleteMovies(movie.id, setState);
                    }}
                  >
                    X
                  </DefaultButton>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </StackItem>

      <StackItem>
        <MovieDetail movieId={selectedMovie} />
      </StackItem>
    </Stack>
  );
}

type ComponentState = {
  data: Movie[];
  loading: boolean;
};

export function fetchMovies(setState: (newState: ComponentState) => void) {
  fetch("https://localhost:7192/api/movies")
    .then((response) => response.json())
    .then((data: MoviesResponse) => {
      const newState: ComponentState = {
        data: data.movies ?? [],
        loading: false,
      };
      setState(newState);
    })
    .catch((e) => {
      console.log("location request failed");
      console.error(e);
    });
}

export function deleteMovies(
  movieId: string | undefined,
  setState: (newState: ComponentState) => void,
) {
  if (movieId === undefined) {
    return;
  }

  fetch("https://localhost:7192/api/movies/" + movieId, { method: "DELETE" })
    .then(() => {
      fetchMovies(setState);
    })
    .catch((e) => {
      console.log("location request failed");
      console.error(e);
    });
}
