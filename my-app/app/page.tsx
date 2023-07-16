"use client";
import Image from "next/image";
import { MovieList } from "./components/MovieList";
import { Stack } from "@fluentui/react";

export default function Home() {
  return (
    <main>
      <Stack>
        <Stack>
          <h2 className={`mb-3 text-2xl font-semibold`}>Movie Tests</h2>
          <p className={`m-0 max-w-[50ch] text-sm opacity-50`}>
            Some test app to test the interactions with my app.
          </p>
        </Stack>

        <MovieList />
      </Stack>
    </main>
  );
}
