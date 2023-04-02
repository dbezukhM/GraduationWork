import { Guid } from "guid-typescript";

export interface EducationalProgramCreate {
    name: string;
    facultyId: Guid;
    specializationId: Guid;
    educationalProgramsTypeId: Guid;
}
