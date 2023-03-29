import { Guid } from "guid-typescript";

export interface Subject {
    id: Guid;
    name: string;
    selectiveBlockName: string;
    educationalProgramName: string;
}
