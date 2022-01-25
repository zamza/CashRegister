import DenominationTypes from '../constants/DenominationTypes';

export default function (name) {
    return DenominationTypes.find(type => type.value === name).display;
}