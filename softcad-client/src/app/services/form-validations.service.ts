export class FormValidationsService {

  constructor() { }

  static getErrorMessage(fieldName: string, validatorName: string, validatorValue: any){
    const config: any = { 
      'required': `O campo ${fieldName} é obrigatório.`,
      'minlength': `O campo ${fieldName} precisa ter no mínimo ${validatorValue.requiredLength} caracteres.`,
      'maxlength': `O campo ${fieldName} precisa ter no máximo ${validatorValue.requiredLength} caracteres.`,
      'cepInvalido': 'CEP inválido!',
      'emailInvalido': 'Email já cadastrado!',
      'equalsTo': 'Campos não são iguais!',
      'pattern': 'Campo inválido!',
      'email': 'Campo email inválido'
    }
    return config[validatorName];
  }    
}
