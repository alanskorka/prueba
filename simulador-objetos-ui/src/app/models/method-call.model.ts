export interface MethodCallModel {
  id?: string;
  methodName?: string;
  referenceType: string;
  referenceName?: string;
  parentMethodId?: string;
  concreteParameterTypes?: string[];
} 