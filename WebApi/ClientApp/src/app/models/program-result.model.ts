import { Guid } from "guid-typescript";

export interface ProgramResult {
    id: Guid;
    name: string;
    description: string;
    educationalProgramId: Guid
}
