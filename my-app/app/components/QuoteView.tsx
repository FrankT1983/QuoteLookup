import { Stack } from "@fluentui/react";
import { Dialog } from "../api/api";

export function QuoteView(props: ComponentProps) {
  if (props.dialog === undefined) {
    return undefined;
  }

  const lines = (props.dialog.lines ?? []).map((l) => (
    <Stack horizontal key={l.secondsAfterStart + " " + l.character?.id}>
      <div>{l.secondsAfterStart}| </div>
      <div>{l.character?.name?.firstName}| </div>
      <div>{l.line}</div>
    </Stack>
  ));

  return (
    <div>
      <Stack>
        <Stack>{lines}</Stack>
      </Stack>
    </div>
  );
}

type ComponentProps = {
  dialog?: Dialog;
};
