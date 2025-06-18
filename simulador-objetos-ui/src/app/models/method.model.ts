import { ParamModel } from './param.model';
import { LocalVarModel } from './local-var.model';
import { MethodCallModel } from './method-call.model';

export interface MethodModel {
  id: number;
  name: string;
  returnType: string;
  parameters: ParamModel[];
  localVars: LocalVarModel[];
  methodCalls: MethodCallModel[];
  isVirtual: boolean;
  isStatic: boolean;
  isOverride: boolean;
  classId: number;
} 