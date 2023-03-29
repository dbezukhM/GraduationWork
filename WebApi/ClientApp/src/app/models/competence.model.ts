import { Guid } from "guid-typescript";

export interface Competence {
    id: Guid;
    name: string;
    description: string;
    educationalProgramId: Guid
}
