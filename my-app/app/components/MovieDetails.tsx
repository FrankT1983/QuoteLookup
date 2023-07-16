import React, { useEffect, useState } from "react";
import { Character, DialogLine, DialogLineType, Movie, MovieResponse, Quote } from "../api/api";
import { DefaultButton, Stack } from "@fluentui/react";
import { QuoteView } from "./QuoteView";
import { faker } from '@faker-js/faker';

const loadingState: ComponentState = {
  data: undefined,
  loading: false,
};

export function MovieDetail(props: ComponentProps) {
  const [state, setState] = useState<ComponentState>(loadingState);

  const [charFirstName, setCharFirstName] = useState("firstname");
  const [charLastName, setCharLastName] = useState("lastname");
  const [charId, setCharId] = useState("id1");

  useEffect(() => {
    fetchMovie(props.movieId, setState);
  }, [props.movieId]);

  if (state.data === undefined) {
    return <div />;
  }

  const quotes = (state.data.quotes ?? []).map((m) => (
    <Stack key={props.movieId + " " + m.id} horizontal>
      <div className={`m-1 `}> {m.id} </div>
      <div className={`m-1 `}>
        {" "}
        <QuoteView dialog={m.dialog} />{" "}
      </div>
      <div>
        <DefaultButton
          onClick={() => {
            deleteQuote(props.movieId, m.id, setState);
          }}
        >
          X
        </DefaultButton>
      </div>
    </Stack>
  ));

  const characters = (state.data.characters ?? []).map((c) => (
    <Stack key={props.movieId + "" + c.id} horizontal>
      <div className={`m-1 `}> {c.id} </div>
      <div className={`m-1 `}>
        {c.name?.firstName} {c.name?.lastName}
      </div>
      <DefaultButton
        onClick={() => {
          deleteCharacter(props.movieId, c.id, setState);
        }}
      >
        X
      </DefaultButton>
    </Stack>
  ));

  return (
    <div>
      <Stack>
        <h3 className={`mb-3 text-2xl font-semibold`}>Movie Details</h3>

        <Stack horizontal>
          <div className={`m-1 `}> Name </div>
          <div className={`m-1 `}> {state.data.name} </div>
        </Stack>

        <Stack horizontal>
          <div className={`m-1 `}> Id </div>
          <div className={`m-1 `}> {state.data.id} </div>
        </Stack>

        <Stack>{quotes}</Stack>
        <DefaultButton
        onClick={() => {
          addRandomQuote(props.movieId, state.data?.characters ?? [], setState )          
        }}
      >Add random quote </DefaultButton>

        <Stack>{characters}</Stack>
        <div>
          <form>
            <input
              type="text"
              value={charFirstName}
              onChange={(e) => setCharFirstName(e.target.value)}
            />

            <input
              type="text"
              value={charLastName}
              onChange={(e) => setCharLastName(e.target.value)}
            />

            <input
              type="text"
              value={charId}
              onChange={(e) => setCharId(e.target.value)}
            />
          </form>
          <DefaultButton
        onClick={() => {
          addCharacter(props.movieId, charId, charFirstName, charLastName , setState)
        }}
      >
        Add Character
      </DefaultButton>
        </div>
      </Stack>
    </div>
  );
}

type ComponentState = {
  data?: Movie;
  loading: boolean;
};

type ComponentProps = {
  movieId?: string;
};

export function fetchMovie(
  id: string | undefined,
  setState: (newState: ComponentState) => void,
) {
  setState(loadingState);
  if (id === undefined) {
    return;
  }

  fetch(`https://localhost:7192/api/Movies/${id}/`)
    .then((response) => response.json())
    .then((data: MovieResponse) => {
      const newState: ComponentState = {
        data: data.movie,
        loading: false,
      };
      setState(newState);
    })
    .catch((e) => {
      console.log("location request failed");
      console.error(e);
    });
}

export function deleteCharacter(
  movieId: string | undefined,
  characterId: string | undefined,
  setState: (newState: ComponentState) => void,
) {
  if (movieId === undefined) {
    return;
  }

  if (characterId === undefined) {
    return;
  }

  fetch(
    "https://localhost:7192/api/movies/" +
      movieId +
      "/characters/" +
      characterId,
    { method: "DELETE" },
  )
    .then(() => {
      fetchMovie(movieId, setState);
    })
    .catch((e) => {
      console.log("location request failed");
      console.error(e);
    });
}

export function deleteQuote(
  movieId: string | undefined,
  quoteId: string | undefined,
  setState: (newState: ComponentState) => void,
) {
  if (movieId === undefined) {
    return;
  }

  if (quoteId === undefined) {
    return;
  }

  fetch("https://localhost:7192/api/movies/" + movieId + "/quotes/" + quoteId, {
    method: "DELETE",
  })
    .then(() => {
      fetchMovie(movieId, setState);
    })
    .catch((e) => {
      console.log("location request failed");
      console.error(e);
    });
}

export function addCharacter(
  movieId: string | undefined,
  characterId: string,
  firstName: string ,
  lastName: string ,
  setState: (newState: ComponentState) => void,
) {
  if (movieId === undefined) {
    return;
  }

  const newCharacater : Character = {
    id : characterId,
    name : {
      firstName : firstName,
      lastName: lastName,
    }
  }

  fetch("https://localhost:7192/api/movies/" + movieId + "/characters/" , {
    method: "PUT",
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(newCharacater)})
    .then(() => {
      fetchMovie(movieId, setState);
    })
    .catch((e) => {
      console.log("location request failed");
      console.error(e);
    });
}

export function addRandomQuote(  
  movieId: string | undefined,  
  characters:  Character[],
  setState: (newState: ComponentState) => void,
) {
  if (movieId === undefined) {
    return;
  }

    const newQuote : Quote = {
    id : faker.string.uuid(),
    dialog:{
      lines: fakeLines(characters)       
    }
  }

  fetch("https://localhost:7192/api/movies/" + movieId + "/quotes/" , {
    method: "PUT",
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(newQuote)})
    .then(() => {
      fetchMovie(movieId, setState);
    })
    .catch((e) => {
      console.log("location request failed");
      console.error(e);
    });
}

function fakeLines(characters : Character[]): DialogLine[] {
  let result : DialogLine[] = [];
  const count = faker.number.int({min:1 , max: 5 })
  for (let i = 0; i < count; i++) {
    result.push({
      character: faker.helpers.arrayElement(characters),
      line : faker.helpers.arrayElement(sampleLines),
      secondsAfterStart: faker.number.float({ min: 0, max: 30000}),    
    })
  }

  return result;
}


const sampleLines = [
  "May the Force be with you." , 
  "There's no place like home.", 
  "I'm the king of the world!" ,
  "Carpe diem. Seize the day, boys. Make your lives extraordinary",
  "Elementary, my dear Watson",
  "It's alive! It's alive!",
  "My mama always said life was like a box of chocolates. You never know what you're gonna get.",
  "I'll be back",
  "You're gonna need a bigger boat.",
  "Here's looking at you, kid"
]