import { ParamModel } from '../services/param.service';

export interface InterfaceMethodModel {
  id: number;
  name: string;
  returnType: string;
  parameters: ParamModel[];
  interfaceId: number;
} 