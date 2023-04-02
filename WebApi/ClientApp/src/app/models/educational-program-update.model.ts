import { Guid } from "guid-typescript";

export interface EducationalProgramUpdate {
    name: string;
    facultyId: Guid;
    specializationId: Guid;
    educationalProgramsTypeId: Guid;
}
