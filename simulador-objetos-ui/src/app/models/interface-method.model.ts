import { ParamModel } from './param.model';

export interface InterfaceMethodModel {
  id: number;
  name: string;
  returnType: string;
  parameters: ParamModel[];
  interfaceId: number;
} 